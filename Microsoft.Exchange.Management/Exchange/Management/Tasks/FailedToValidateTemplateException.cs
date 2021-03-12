using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E1 RID: 4321
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToValidateTemplateException : LocalizedException
	{
		// Token: 0x0600B351 RID: 45905 RVA: 0x0029B03C File Offset: 0x0029923C
		public FailedToValidateTemplateException(Guid templateId, WellKnownErrorCode eCode) : base(Strings.FailedToValidateTemplate(templateId, eCode))
		{
			this.templateId = templateId;
			this.eCode = eCode;
		}

		// Token: 0x0600B352 RID: 45906 RVA: 0x0029B059 File Offset: 0x00299259
		public FailedToValidateTemplateException(Guid templateId, WellKnownErrorCode eCode, Exception innerException) : base(Strings.FailedToValidateTemplate(templateId, eCode), innerException)
		{
			this.templateId = templateId;
			this.eCode = eCode;
		}

		// Token: 0x0600B353 RID: 45907 RVA: 0x0029B078 File Offset: 0x00299278
		protected FailedToValidateTemplateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.templateId = (Guid)info.GetValue("templateId", typeof(Guid));
			this.eCode = (WellKnownErrorCode)info.GetValue("eCode", typeof(WellKnownErrorCode));
		}

		// Token: 0x0600B354 RID: 45908 RVA: 0x0029B0CD File Offset: 0x002992CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("templateId", this.templateId);
			info.AddValue("eCode", this.eCode);
		}

		// Token: 0x170038EA RID: 14570
		// (get) Token: 0x0600B355 RID: 45909 RVA: 0x0029B103 File Offset: 0x00299303
		public Guid TemplateId
		{
			get
			{
				return this.templateId;
			}
		}

		// Token: 0x170038EB RID: 14571
		// (get) Token: 0x0600B356 RID: 45910 RVA: 0x0029B10B File Offset: 0x0029930B
		public WellKnownErrorCode ECode
		{
			get
			{
				return this.eCode;
			}
		}

		// Token: 0x04006250 RID: 25168
		private readonly Guid templateId;

		// Token: 0x04006251 RID: 25169
		private readonly WellKnownErrorCode eCode;
	}
}
