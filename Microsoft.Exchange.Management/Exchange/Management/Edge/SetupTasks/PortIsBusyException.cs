using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200122D RID: 4653
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PortIsBusyException : LocalizedException
	{
		// Token: 0x0600BB40 RID: 47936 RVA: 0x002A9FB5 File Offset: 0x002A81B5
		public PortIsBusyException(int port) : base(Strings.PortIsBusy(port))
		{
			this.port = port;
		}

		// Token: 0x0600BB41 RID: 47937 RVA: 0x002A9FCA File Offset: 0x002A81CA
		public PortIsBusyException(int port, Exception innerException) : base(Strings.PortIsBusy(port), innerException)
		{
			this.port = port;
		}

		// Token: 0x0600BB42 RID: 47938 RVA: 0x002A9FE0 File Offset: 0x002A81E0
		protected PortIsBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.port = (int)info.GetValue("port", typeof(int));
		}

		// Token: 0x0600BB43 RID: 47939 RVA: 0x002AA00A File Offset: 0x002A820A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("port", this.port);
		}

		// Token: 0x17003AEC RID: 15084
		// (get) Token: 0x0600BB44 RID: 47940 RVA: 0x002AA025 File Offset: 0x002A8225
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x04006574 RID: 25972
		private readonly int port;
	}
}
