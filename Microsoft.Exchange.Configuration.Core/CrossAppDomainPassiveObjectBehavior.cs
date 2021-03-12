using System;
using System.Collections.Generic;
using System.IO.Pipes;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000005 RID: 5
	internal class CrossAppDomainPassiveObjectBehavior : CrossAppDomainObjectBehavior
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002308 File Offset: 0x00000508
		internal CrossAppDomainPassiveObjectBehavior(string namedPipeName, BehaviorDirection direction) : base(namedPipeName, direction)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023C4 File Offset: 0x000005C4
		internal IEnumerable<string> RecieveMessages()
		{
			return CoreLogger.ExecuteAndLog<IEnumerable<string>>("CrossAppDomainPassiveObjectBehavior.ReceiveMessage", true, null, null, null, delegate()
			{
				IEnumerable<string> result;
				using (NamedPipeClientStream clientStream = new NamedPipeClientStream(".", base.NamedPipeName, PipeDirection.In))
				{
					CrossAppDomainObjectBehavior.ConnectClientStream(clientStream, 1000, base.NamedPipeName, false);
					byte[] array = CrossAppDomainObjectBehavior.LoopReadData((byte[] buffer, int offset, int count) => clientStream.Read(buffer, offset, count));
					if (array == null || array.Length == 0)
					{
						result = null;
					}
					else
					{
						result = CrossAppDomainObjectBehavior.UnpackMessages(array);
					}
				}
				return result;
			});
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002464 File Offset: 0x00000664
		internal void SendMessage(byte[] message)
		{
			CoreLogger.ExecuteAndLog("CrossAppDomainPassiveObjectBehavior.ReceiveMessage", true, null, null, delegate()
			{
				using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", this.NamedPipeName, PipeDirection.Out))
				{
					if (CrossAppDomainObjectBehavior.ConnectClientStream(namedPipeClientStream, 1000, this.NamedPipeName, false))
					{
						namedPipeClientStream.Write(message, 0, message.Length);
					}
				}
			});
		}

		// Token: 0x04000008 RID: 8
		private const int DefaultNamedPipeTimeOutInMilliseconds = 1000;
	}
}
