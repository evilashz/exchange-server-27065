using System;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.RightsManagement
{
	// Token: 0x0200029C RID: 668
	[Serializable]
	public sealed class RmsTemplateIdentity : ObjectId, IEquatable<RmsTemplateIdentity>
	{
		// Token: 0x06001835 RID: 6197 RVA: 0x0004C370 File Offset: 0x0004A570
		public RmsTemplateIdentity(Guid templateId, string templateName, RmsTemplateType templateType)
		{
			this.templateId = templateId;
			this.templateName = templateName;
			this.templateType = templateType;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0004C38D File Offset: 0x0004A58D
		public RmsTemplateIdentity(Guid templateId, string templateName) : this(templateId, templateName, RmsTemplateType.Distributed)
		{
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0004C398 File Offset: 0x0004A598
		public RmsTemplateIdentity(Guid templateId) : this(templateId, string.Empty)
		{
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0004C3A6 File Offset: 0x0004A5A6
		public RmsTemplateIdentity() : this(Guid.Empty, string.Empty)
		{
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0004C3B8 File Offset: 0x0004A5B8
		public Guid TemplateId
		{
			get
			{
				return this.templateId;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0004C3C0 File Offset: 0x0004A5C0
		public string TemplateName
		{
			get
			{
				return this.templateName;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0004C3C8 File Offset: 0x0004A5C8
		public RmsTemplateType Type
		{
			get
			{
				return this.templateType;
			}
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
		public override byte[] GetBytes()
		{
			return this.templateId.ToByteArray();
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0004C3DD File Offset: 0x0004A5DD
		public bool Equals(RmsTemplateIdentity other)
		{
			return other != null && this.templateId.Equals(other.templateId);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0004C3F5 File Offset: 0x0004A5F5
		public override bool Equals(object other)
		{
			return this.Equals(other as RmsTemplateIdentity);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0004C403 File Offset: 0x0004A603
		public override int GetHashCode()
		{
			return this.templateId.GetHashCode();
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0004C416 File Offset: 0x0004A616
		public override string ToString()
		{
			return this.templateName;
		}

		// Token: 0x04000E41 RID: 3649
		private Guid templateId;

		// Token: 0x04000E42 RID: 3650
		private string templateName;

		// Token: 0x04000E43 RID: 3651
		private RmsTemplateType templateType;
	}
}
