using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000056 RID: 86
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RemoteServerTooSlowException : LocalizedException
	{
		// Token: 0x06000239 RID: 569 RVA: 0x000065FE File Offset: 0x000047FE
		public RemoteServerTooSlowException(string remoteServer, int port, TimeSpan actualLatency, TimeSpan expectedLatency) : base(Strings.RemoteServerTooSlowException(remoteServer, port, actualLatency, expectedLatency))
		{
			this.remoteServer = remoteServer;
			this.port = port;
			this.actualLatency = actualLatency;
			this.expectedLatency = expectedLatency;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000662D File Offset: 0x0000482D
		public RemoteServerTooSlowException(string remoteServer, int port, TimeSpan actualLatency, TimeSpan expectedLatency, Exception innerException) : base(Strings.RemoteServerTooSlowException(remoteServer, port, actualLatency, expectedLatency), innerException)
		{
			this.remoteServer = remoteServer;
			this.port = port;
			this.actualLatency = actualLatency;
			this.expectedLatency = expectedLatency;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006660 File Offset: 0x00004860
		protected RemoteServerTooSlowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteServer = (string)info.GetValue("remoteServer", typeof(string));
			this.port = (int)info.GetValue("port", typeof(int));
			this.actualLatency = (TimeSpan)info.GetValue("actualLatency", typeof(TimeSpan));
			this.expectedLatency = (TimeSpan)info.GetValue("expectedLatency", typeof(TimeSpan));
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000066F8 File Offset: 0x000048F8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteServer", this.remoteServer);
			info.AddValue("port", this.port);
			info.AddValue("actualLatency", this.actualLatency);
			info.AddValue("expectedLatency", this.expectedLatency);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000675B File Offset: 0x0000495B
		public string RemoteServer
		{
			get
			{
				return this.remoteServer;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00006763 File Offset: 0x00004963
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000676B File Offset: 0x0000496B
		public TimeSpan ActualLatency
		{
			get
			{
				return this.actualLatency;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00006773 File Offset: 0x00004973
		public TimeSpan ExpectedLatency
		{
			get
			{
				return this.expectedLatency;
			}
		}

		// Token: 0x040000FE RID: 254
		private readonly string remoteServer;

		// Token: 0x040000FF RID: 255
		private readonly int port;

		// Token: 0x04000100 RID: 256
		private readonly TimeSpan actualLatency;

		// Token: 0x04000101 RID: 257
		private readonly TimeSpan expectedLatency;
	}
}
