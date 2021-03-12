using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x020010CC RID: 4300
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OutlookProtectionRuleNameIsNotUniqueException : LocalizedException
	{
		// Token: 0x0600B2F1 RID: 45809 RVA: 0x0029A8CA File Offset: 0x00298ACA
		public OutlookProtectionRuleNameIsNotUniqueException(string name) : base(Strings.OutlookProtectionRuleNameIsNotUnique(name))
		{
			this.name = name;
		}

		// Token: 0x0600B2F2 RID: 45810 RVA: 0x0029A8DF File Offset: 0x00298ADF
		public OutlookProtectionRuleNameIsNotUniqueException(string name, Exception innerException) : base(Strings.OutlookProtectionRuleNameIsNotUnique(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B2F3 RID: 45811 RVA: 0x0029A8F5 File Offset: 0x00298AF5
		protected OutlookProtectionRuleNameIsNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B2F4 RID: 45812 RVA: 0x0029A91F File Offset: 0x00298B1F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170038DE RID: 14558
		// (get) Token: 0x0600B2F5 RID: 45813 RVA: 0x0029A93A File Offset: 0x00298B3A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006244 RID: 25156
		private readonly string name;
	}
}
