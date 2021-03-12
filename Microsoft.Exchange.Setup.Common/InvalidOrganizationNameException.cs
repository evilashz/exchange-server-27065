using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200007B RID: 123
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidOrganizationNameException : LocalizedException
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x00016608 File Offset: 0x00014808
		public InvalidOrganizationNameException(string name) : base(Strings.InvalidOrganizationName(name))
		{
			this.name = name;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001661D File Offset: 0x0001481D
		public InvalidOrganizationNameException(string name, Exception innerException) : base(Strings.InvalidOrganizationName(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00016633 File Offset: 0x00014833
		protected InvalidOrganizationNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001665D File Offset: 0x0001485D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00016678 File Offset: 0x00014878
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040002FB RID: 763
		private readonly string name;
	}
}
