using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A99 RID: 2713
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DefaultAdministrativeGroupNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FCA RID: 32714 RVA: 0x001A475B File Offset: 0x001A295B
		public DefaultAdministrativeGroupNotFoundException(string agName) : base(DirectoryStrings.DefaultAdministrativeGroupNotFoundException(agName))
		{
			this.agName = agName;
		}

		// Token: 0x06007FCB RID: 32715 RVA: 0x001A4770 File Offset: 0x001A2970
		public DefaultAdministrativeGroupNotFoundException(string agName, Exception innerException) : base(DirectoryStrings.DefaultAdministrativeGroupNotFoundException(agName), innerException)
		{
			this.agName = agName;
		}

		// Token: 0x06007FCC RID: 32716 RVA: 0x001A4786 File Offset: 0x001A2986
		protected DefaultAdministrativeGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.agName = (string)info.GetValue("agName", typeof(string));
		}

		// Token: 0x06007FCD RID: 32717 RVA: 0x001A47B0 File Offset: 0x001A29B0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("agName", this.agName);
		}

		// Token: 0x17002EBD RID: 11965
		// (get) Token: 0x06007FCE RID: 32718 RVA: 0x001A47CB File Offset: 0x001A29CB
		public string AgName
		{
			get
			{
				return this.agName;
			}
		}

		// Token: 0x04005597 RID: 21911
		private readonly string agName;
	}
}
