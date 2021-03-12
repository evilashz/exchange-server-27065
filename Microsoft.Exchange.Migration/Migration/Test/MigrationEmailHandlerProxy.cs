using System;
using System.Net.Sockets;
using System.Runtime.Remoting;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration.Test
{
	// Token: 0x020000CE RID: 206
	internal class MigrationEmailHandlerProxy : IMigrationEmailHandler
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x0002F150 File Offset: 0x0002D350
		private MigrationEmailHandlerProxy(string endpoint)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(endpoint, "endpoint");
			this.endpoint = endpoint;
			this.implementation = (IMigrationEmailHandler)Activator.GetObject(typeof(IMigrationEmailHandler), endpoint);
			if (this.implementation == null)
			{
				throw new InvalidOperationException("couldn't create remote instance at endpoint " + endpoint);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002F1AC File Offset: 0x0002D3AC
		public static bool TryCreate(string reportMessageEndpoint, out IMigrationEmailHandler handler)
		{
			handler = null;
			if (string.IsNullOrWhiteSpace(reportMessageEndpoint))
			{
				return false;
			}
			bool result;
			try
			{
				handler = new MigrationEmailHandlerProxy(reportMessageEndpoint);
				result = true;
			}
			catch (RemotingException exception)
			{
				MigrationLogger.Log(MigrationEventType.Error, exception, "Failed to open connection to emulator even though one was set.", new object[0]);
				result = false;
			}
			return result;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002F1FC File Offset: 0x0002D3FC
		public IMigrationEmailMessageItem CreateEmailMessage()
		{
			IMigrationEmailMessageItem result;
			try
			{
				result = this.implementation.CreateEmailMessage();
			}
			catch (RemotingException innerException)
			{
				throw new MigrationServerConnectionFailedException(this.endpoint, innerException);
			}
			catch (SocketException innerException2)
			{
				throw new MigrationServerConnectionFailedException(this.endpoint, innerException2);
			}
			return result;
		}

		// Token: 0x04000434 RID: 1076
		private readonly IMigrationEmailHandler implementation;

		// Token: 0x04000435 RID: 1077
		private readonly string endpoint;
	}
}
