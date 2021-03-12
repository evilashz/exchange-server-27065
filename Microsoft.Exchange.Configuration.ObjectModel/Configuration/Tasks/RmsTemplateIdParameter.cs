using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	public sealed class RmsTemplateIdParameter : IIdentityParameter
	{
		// Token: 0x06000B71 RID: 2929 RVA: 0x00024578 File Offset: 0x00022778
		public RmsTemplateIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (identity.Length == 0)
			{
				throw new ArgumentException(Strings.ErrorEmptyParameter(base.GetType().ToString()), "identity");
			}
			this.rawIdentity = identity;
			this.Parse(identity);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000245D0 File Offset: 0x000227D0
		public RmsTemplateIdParameter(RmsTemplatePresentation template)
		{
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			RmsTemplateIdentity rmsTemplateIdentity = (RmsTemplateIdentity)template.Identity;
			this.rawIdentity = rmsTemplateIdentity.ToString();
			this.templateId = rmsTemplateIdentity.TemplateId;
			this.templateName = template.Name;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00024621 File Offset: 0x00022821
		public RmsTemplateIdParameter(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			this.rawIdentity = objectId.ToString();
			((IIdentityParameter)this).Initialize(objectId);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002464A File Offset: 0x0002284A
		public RmsTemplateIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.rawIdentity = namedIdentity.DisplayName;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00024664 File Offset: 0x00022864
		public RmsTemplateIdParameter()
		{
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0002466C File Offset: 0x0002286C
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00024674 File Offset: 0x00022874
		public override string ToString()
		{
			return this.rawIdentity;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0002467C File Offset: 0x0002287C
		public Guid TemplateId
		{
			get
			{
				return this.templateId;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00024684 File Offset: 0x00022884
		public string TemplateName
		{
			get
			{
				return this.templateName;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002468C File Offset: 0x0002288C
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session)
		{
			LocalizedString? localizedString;
			return ((IIdentityParameter)this).GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000246A4 File Offset: 0x000228A4
		IEnumerable<T> IIdentityParameter.GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			QueryFilter queryFilter = new RmsTemplateQueryFilter(this.templateId, this.templateName);
			notFoundReason = new LocalizedString?(Strings.ErrorManagementObjectNotFound(this.ToString()));
			if (optionalData != null && optionalData.AdditionalFilter != null)
			{
				queryFilter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					optionalData.AdditionalFilter
				});
			}
			return session.FindPaged<T>(queryFilter, rootId, false, null, 0);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002470C File Offset: 0x0002290C
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			RmsTemplateIdentity rmsTemplateIdentity = objectId as RmsTemplateIdentity;
			if (rmsTemplateIdentity == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterType("objectId", typeof(RmsTemplateIdentity).Name), "objectId");
			}
			this.rawIdentity = rmsTemplateIdentity.ToString();
			this.templateName = rmsTemplateIdentity.TemplateName;
			this.templateId = rmsTemplateIdentity.TemplateId;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00024770 File Offset: 0x00022970
		private void Parse(string identity)
		{
			if (!DrmClientUtils.TryParseGuid(identity, out this.templateId))
			{
				this.templateId = Guid.Empty;
			}
			this.templateName = (string.IsNullOrEmpty(identity) ? null : identity.Trim());
		}

		// Token: 0x040002A1 RID: 673
		private Guid templateId;

		// Token: 0x040002A2 RID: 674
		private string templateName;

		// Token: 0x040002A3 RID: 675
		private string rawIdentity;
	}
}
