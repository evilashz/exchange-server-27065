using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A9A RID: 2714
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DefaultDatabaseContainerNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FCF RID: 32719 RVA: 0x001A47D3 File Offset: 0x001A29D3
		public DefaultDatabaseContainerNotFoundException(string agName) : base(DirectoryStrings.DefaultDatabaseContainerNotFoundException(agName))
		{
			this.agName = agName;
		}

		// Token: 0x06007FD0 RID: 32720 RVA: 0x001A47E8 File Offset: 0x001A29E8
		public DefaultDatabaseContainerNotFoundException(string agName, Exception innerException) : base(DirectoryStrings.DefaultDatabaseContainerNotFoundException(agName), innerException)
		{
			this.agName = agName;
		}

		// Token: 0x06007FD1 RID: 32721 RVA: 0x001A47FE File Offset: 0x001A29FE
		protected DefaultDatabaseContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.agName = (string)info.GetValue("agName", typeof(string));
		}

		// Token: 0x06007FD2 RID: 32722 RVA: 0x001A4828 File Offset: 0x001A2A28
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("agName", this.agName);
		}

		// Token: 0x17002EBE RID: 11966
		// (get) Token: 0x06007FD3 RID: 32723 RVA: 0x001A4843 File Offset: 0x001A2A43
		public string AgName
		{
			get
			{
				return this.agName;
			}
		}

		// Token: 0x04005598 RID: 21912
		private readonly string agName;
	}
}
