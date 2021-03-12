using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001010 RID: 4112
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class E4eRuleRmsTemplateNotFoundException : LocalizedException
	{
		// Token: 0x0600AF0C RID: 44812 RVA: 0x00293CEF File Offset: 0x00291EEF
		public E4eRuleRmsTemplateNotFoundException(string name) : base(Strings.E4eRuleRmsTemplateNotFound(name))
		{
			this.name = name;
		}

		// Token: 0x0600AF0D RID: 44813 RVA: 0x00293D04 File Offset: 0x00291F04
		public E4eRuleRmsTemplateNotFoundException(string name, Exception innerException) : base(Strings.E4eRuleRmsTemplateNotFound(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AF0E RID: 44814 RVA: 0x00293D1A File Offset: 0x00291F1A
		protected E4eRuleRmsTemplateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AF0F RID: 44815 RVA: 0x00293D44 File Offset: 0x00291F44
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170037E9 RID: 14313
		// (get) Token: 0x0600AF10 RID: 44816 RVA: 0x00293D5F File Offset: 0x00291F5F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400614F RID: 24911
		private readonly string name;
	}
}
