using ProtoBuf;
using System;
using Quasar.Common.Messages.other;

using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Quasar.Common.Messages
{
    [ProtoContract]
    public class ExceptionMessage : IMessage
    {
        [ProtoMember(1)]
        public string Trace { get; set; }

        [ProtoMember(2)]
        public string Error { get; set; }

        [ProtoMember(3)]
        public string Raw { get; set; }

        [ProtoMember(4)]
        public string Level { get; set; }

        public ExceptionMessage() { }

        public ExceptionMessage(Exception ex)
        {
            string namespacePrefix = "Quasar";
            var stackTrace = new StackTrace(ex, true);
            var frames = stackTrace.GetFrames();
            if (frames == null)
                return;

            var filteredFrames = frames.Where(frame =>
            {
                MethodBase method = frame.GetMethod();
                if (method?.DeclaringType == null)
                    return false;
                return method.DeclaringType.Namespace?.StartsWith(namespacePrefix) ?? false;
            });

            var result = string.Join(Environment.NewLine, filteredFrames.Select(frame =>
            {
                MethodBase method = frame.GetMethod();
                string typeName = method.DeclaringType.FullName;
                string methodName = method.Name;
                int lineNumber = frame.GetFileLineNumber();
                return $"{typeName}.{methodName} (line {lineNumber})";
            }));

            return;
        }
    }
}
