using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A9C RID: 2716
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DefaultDatabaseAvailabilityGroupContainerNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FD9 RID: 32729 RVA: 0x001A48C3 File Offset: 0x001A2AC3
		public DefaultDatabaseAvailabilityGroupContainerNotFoundException(string agName) : base(DirectoryStrings.DefaultDatabaseAvailabilityGroupContainerNotFoundException(agName))
		{
			this.agName = agName;
		}

		// Token: 0x06007FDA RID: 32730 RVA: 0x001A48D8 File Offset: 0x001A2AD8
		public DefaultDatabaseAvailabilityGroupContainerNotFoundException(string agName, Exception innerException) : base(DirectoryStrings.DefaultDatabaseAvailabilityGroupContainerNotFoundException(agName), innerException)
		{
			this.agName = agName;
		}

		// Token: 0x06007FDB RID: 32731 RVA: 0x001A48EE File Offset: 0x001A2AEE
		protected DefaultDatabaseAvailabilityGroupContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.agName = (string)info.GetValue("agName", typeof(string));
		}

		// Token: 0x06007FDC RID: 32732 RVA: 0x001A4918 File Offset: 0x001A2B18
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("agName", this.agName);
		}

		// Token: 0x17002EC0 RID: 11968
		// (get) Token: 0x06007FDD RID: 32733 RVA: 0x001A4933 File Offset: 0x001A2B33
		public string AgName
		{
			get
			{
				return this.agName;
			}
		}

		// Token: 0x0400559A RID: 21914
		private readonly string agName;
	}
}
