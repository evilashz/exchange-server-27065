using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009B8 RID: 2488
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class RmsTemplate : CachableItem, IEquatable<RmsTemplate>
	{
		// Token: 0x06003608 RID: 13832 RVA: 0x00089710 File Offset: 0x00087910
		internal RmsTemplate(Guid id) : this(id, RmsTemplateType.Distributed)
		{
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x0008971A File Offset: 0x0008791A
		internal RmsTemplate(Guid id, RmsTemplateType type)
		{
			this.id = id;
			if (type == RmsTemplateType.All)
			{
				throw new ArgumentException("Only Archived and Distributed template types supported.", "type");
			}
			this.type = type;
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x0600360A RID: 13834 RVA: 0x00089745 File Offset: 0x00087945
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x0008974D File Offset: 0x0008794D
		public RmsTemplateType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x0600360C RID: 13836 RVA: 0x00089755 File Offset: 0x00087955
		public virtual string Name
		{
			get
			{
				return this.GetName(CultureInfo.CurrentUICulture);
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x00089762 File Offset: 0x00087962
		public virtual string Description
		{
			get
			{
				return this.GetDescription(CultureInfo.CurrentUICulture);
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x0008976F File Offset: 0x0008796F
		public override long ItemSize
		{
			get
			{
				return (long)(this.GetSize() + 16);
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x0600360F RID: 13839
		public abstract bool RequiresRepublishingWhenRecipientsChange { get; }

		// Token: 0x06003610 RID: 13840 RVA: 0x0008977B File Offset: 0x0008797B
		public static RmsTemplate Create(Guid id, string template)
		{
			if (id == RmsTemplate.DoNotForward.Id)
			{
				return RmsTemplate.DoNotForward;
			}
			if (id == RmsTemplate.InternetConfidential.Id)
			{
				return RmsTemplate.InternetConfidential;
			}
			return new RmsTemplate.ServerRmsTemplate(id, template, RmsTemplateType.Distributed);
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000897B8 File Offset: 0x000879B8
		public static RmsTemplate CreateFromPublishLicense(string publishLicense)
		{
			if (string.IsNullOrEmpty(publishLicense))
			{
				throw new ArgumentException("publishLicense");
			}
			ICollection<RmsTemplate.LocaleNameDescription> templateNamesAndDescriptions = RmsTemplate.GetTemplateNamesAndDescriptions(publishLicense);
			string cultureNeutralName = RmsTemplate.GetCultureNeutralName(templateNamesAndDescriptions);
			if (string.Equals("Do Not Forward", cultureNeutralName, StringComparison.OrdinalIgnoreCase))
			{
				return RmsTemplate.DoNotForward;
			}
			if (string.Equals("Internet Confidential", cultureNeutralName, StringComparison.OrdinalIgnoreCase))
			{
				return RmsTemplate.InternetConfidential;
			}
			Guid templateGuidFromLicense = DrmClientUtils.GetTemplateGuidFromLicense(publishLicense);
			if (templateGuidFromLicense == Guid.Empty)
			{
				return RmsTemplate.DoNotForward;
			}
			return new RmsTemplate.ServerRmsTemplate(templateGuidFromLicense, publishLicense, templateNamesAndDescriptions);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x00089830 File Offset: 0x00087A30
		public static RmsTemplate CreateServerTemplateFromTemplateDefinition(string templateXrml, RmsTemplateType type)
		{
			if (string.IsNullOrEmpty(templateXrml))
			{
				throw new ArgumentNullException("templateXrml");
			}
			Guid templateGuidFromLicense = DrmClientUtils.GetTemplateGuidFromLicense(templateXrml);
			if (templateGuidFromLicense == Guid.Empty)
			{
				throw new RightsManagementException(RightsManagementFailureCode.InvalidLicense, DrmStrings.FailedToGetTemplateIdFromLicense);
			}
			return new RmsTemplate.ServerRmsTemplate(templateGuidFromLicense, templateXrml, type);
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x0008987C File Offset: 0x00087A7C
		public string CreatePublishLicense(string sender, string from, IEnumerable<string> recipients, SymmetricSecurityKey cek, DisposableTenantLicensePair tenantLicenses, SafeRightsManagementEnvironmentHandle envHandle, out string ownerUseLicense, out string contentId, out string contentIdType)
		{
			if (envHandle == null)
			{
				throw new ArgumentNullException("envHandle");
			}
			if (tenantLicenses == null)
			{
				throw new ArgumentNullException("tenantLicenses");
			}
			if (RmsAppSettings.Instance.IsSamlAuthenticationEnabledForInternalRMS)
			{
				throw new RightsManagementException(RightsManagementFailureCode.ActionNotSupported, new LocalizedString("Publishing is not allowed while RmsEnableSamlAuthenticationforInternalRMS is set in the app config."));
			}
			if (tenantLicenses.EnablingPrincipalRac == null || tenantLicenses.EnablingPrincipalRac.IsInvalid || tenantLicenses.BoundLicenseClc == null || tenantLicenses.BoundLicenseClc.IsInvalid)
			{
				throw new ArgumentException("tenantLicenses");
			}
			SafeRightsManagementPubHandle safeRightsManagementPubHandle = null;
			SafeRightsManagementPubHandle safeRightsManagementPubHandle2 = null;
			SafeRightsManagementPubHandle safeRightsManagementPubHandle3 = null;
			string callbackData;
			try
			{
				if (!string.IsNullOrEmpty(sender) && !string.Equals(sender, "<>", StringComparison.OrdinalIgnoreCase))
				{
					Errors.ThrowOnErrorCode(SafeNativeMethods.DRMCreateUser(sender, null, "Unspecified", out safeRightsManagementPubHandle2));
				}
				if (!string.IsNullOrEmpty(from) && !string.Equals(from, sender, StringComparison.OrdinalIgnoreCase) && !string.Equals(from, "<>", StringComparison.OrdinalIgnoreCase))
				{
					Errors.ThrowOnErrorCode(SafeNativeMethods.DRMCreateUser(from, null, "Unspecified", out safeRightsManagementPubHandle3));
				}
				safeRightsManagementPubHandle = this.CreateUnsignedIssuanceLicense(safeRightsManagementPubHandle2, safeRightsManagementPubHandle3, recipients);
				contentId = Guid.NewGuid().ToString("B");
				contentIdType = "MS-GUID";
				Errors.ThrowOnErrorCode(SafeNativeMethods.DRMSetMetaData(safeRightsManagementPubHandle, contentId, "MS-GUID", null, null, "Microsoft Office Document", "Microsoft Office Document"));
				using (CallbackHandler callbackHandler = new CallbackHandler())
				{
					SignIssuanceLicenseFlags signIssuanceLicenseFlags = SignIssuanceLicenseFlags.Offline | SignIssuanceLicenseFlags.OwnerLicenseNoPersist;
					if (cek == null)
					{
						signIssuanceLicenseFlags |= SignIssuanceLicenseFlags.AutoGenerateKey;
					}
					Errors.ThrowOnErrorCode(SafeNativeMethods.DRMGetSignedIssuanceLicenseEx(envHandle, safeRightsManagementPubHandle, signIssuanceLicenseFlags, (cek != null) ? cek.GetSymmetricKey() : null, (uint)((cek != null) ? (cek.KeySize / 8) : 0), "AES", IntPtr.Zero, tenantLicenses.EnablingPrincipalRac, tenantLicenses.BoundLicenseClc, callbackHandler.CallbackDelegate, IntPtr.Zero));
					callbackHandler.WaitForCompletion();
					callbackData = callbackHandler.CallbackData;
					ownerUseLicense = DrmClientUtils.GetOwnerLicense(safeRightsManagementPubHandle);
				}
			}
			finally
			{
				if (safeRightsManagementPubHandle != null)
				{
					safeRightsManagementPubHandle.Close();
				}
				if (safeRightsManagementPubHandle2 != null)
				{
					safeRightsManagementPubHandle2.Close();
				}
				if (safeRightsManagementPubHandle3 != null)
				{
					safeRightsManagementPubHandle3.Close();
				}
			}
			return callbackData;
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x00089A8C File Offset: 0x00087C8C
		public string GetName(CultureInfo locale)
		{
			string result;
			string text;
			this.GetNameAndDescription(locale, out result, out text);
			return result;
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x00089AA8 File Offset: 0x00087CA8
		public string GetDescription(CultureInfo locale)
		{
			string text;
			string result;
			this.GetNameAndDescription(locale, out text, out result);
			return result;
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x00089AC1 File Offset: 0x00087CC1
		public bool Equals(RmsTemplate other)
		{
			return other != null && other.Id == this.id;
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x00089AD9 File Offset: 0x00087CD9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RmsTemplate);
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x00089AE8 File Offset: 0x00087CE8
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		}

		// Token: 0x06003619 RID: 13849
		protected abstract SafeRightsManagementPubHandle CreateUnsignedIssuanceLicense(SafeRightsManagementPubHandle ownerHandle, SafeRightsManagementPubHandle fromHandle, IEnumerable<string> recipients);

		// Token: 0x0600361A RID: 13850
		protected abstract void GetNameAndDescription(CultureInfo locale, out string templateName, out string templateDescription);

		// Token: 0x0600361B RID: 13851
		protected abstract int GetSize();

		// Token: 0x0600361C RID: 13852 RVA: 0x00089B09 File Offset: 0x00087D09
		private static RmsTemplate CreateDoNotForwardTemplate()
		{
			return new RmsTemplate.OneOffRmsTemplate(new Guid("CF5CF348-A8D7-40D5-91EF-A600B88A395D"), "Do Not Forward", SystemMessages.DoNotForwardName, SystemMessages.DoNotForwardDescription, RightUtils.GetIndividualRightNames(RmsTemplate.DoNotForwardRights));
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x00089B33 File Offset: 0x00087D33
		private static RmsTemplate CreateInternetConfidentialTemplate()
		{
			return new RmsTemplate.OneOffRmsTemplate(new Guid("81E24817-F117-4943-8959-60E1477E67B6"), "Internet Confidential", SystemMessages.InternetConfidentialName, SystemMessages.InternetConfidentialDescription, RightUtils.GetIndividualRightNames(RmsTemplate.InternetConfidentialRights));
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x00089B5D File Offset: 0x00087D5D
		private static RmsTemplate CreateEmptyTemplate()
		{
			return new RmsTemplate.OneOffRmsTemplate(Guid.Empty, string.Empty, new LocalizedString(string.Empty), new LocalizedString(string.Empty), new string[0]);
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x00089B88 File Offset: 0x00087D88
		private static ICollection<RmsTemplate.LocaleNameDescription> GetTemplateNamesAndDescriptions(string template)
		{
			if (string.IsNullOrEmpty(template))
			{
				throw new ArgumentNullException("template");
			}
			SafeRightsManagementPubHandle safeRightsManagementPubHandle = null;
			ICollection<RmsTemplate.LocaleNameDescription> result;
			try
			{
				int hr = SafeNativeMethods.DRMCreateIssuanceLicense(null, null, null, null, SafeRightsManagementPubHandle.InvalidHandle, template, SafeRightsManagementHandle.InvalidHandle, out safeRightsManagementPubHandle);
				Errors.ThrowOnErrorCode(hr);
				LinkedList<RmsTemplate.LocaleNameDescription> linkedList = new LinkedList<RmsTemplate.LocaleNameDescription>();
				using (safeRightsManagementPubHandle)
				{
					uint num = 0U;
					uint localeId;
					string name;
					string description;
					while (DrmClientUtils.GetNameAndDescription(safeRightsManagementPubHandle, num, out localeId, out name, out description))
					{
						linkedList.AddLast(new RmsTemplate.LocaleNameDescription(localeId, name, description));
						num += 1U;
					}
				}
				result = linkedList;
			}
			finally
			{
				if (safeRightsManagementPubHandle != null)
				{
					safeRightsManagementPubHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x00089C34 File Offset: 0x00087E34
		private static string GetCultureNeutralName(IEnumerable<RmsTemplate.LocaleNameDescription> localeNameDescriptions)
		{
			if (localeNameDescriptions == null)
			{
				return string.Empty;
			}
			foreach (RmsTemplate.LocaleNameDescription localeNameDescription in localeNameDescriptions)
			{
				if (localeNameDescription.LocaleId == 0U)
				{
					return localeNameDescription.Name;
				}
			}
			return string.Empty;
		}

		// Token: 0x04002E7C RID: 11900
		internal const string DoNotForwardTemplateId = "CF5CF348-A8D7-40D5-91EF-A600B88A395D";

		// Token: 0x04002E7D RID: 11901
		internal const string InternetConfidentialId = "81E24817-F117-4943-8959-60E1477E67B6";

		// Token: 0x04002E7E RID: 11902
		private const string NullReversePath = "<>";

		// Token: 0x04002E7F RID: 11903
		private const string DoNotForwardCultureNeutralName = "Do Not Forward";

		// Token: 0x04002E80 RID: 11904
		private const string InternetConfidentialCultureNeutralName = "Internet Confidential";

		// Token: 0x04002E81 RID: 11905
		public static readonly IEnumerable<CultureInfo> SupportedClientLanguages = LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client);

		// Token: 0x04002E82 RID: 11906
		internal static readonly ContentRight DoNotForwardRights = ContentRight.View | ContentRight.Edit | ContentRight.ObjectModel | ContentRight.ViewRightsData | ContentRight.Reply | ContentRight.ReplyAll | ContentRight.DocumentEdit;

		// Token: 0x04002E83 RID: 11907
		internal static readonly ContentRight InternetConfidentialRights = ContentRight.View | ContentRight.Edit | ContentRight.Print | ContentRight.Extract | ContentRight.ObjectModel | ContentRight.ViewRightsData | ContentRight.Reply | ContentRight.ReplyAll | ContentRight.Sign | ContentRight.DocumentEdit | ContentRight.Export;

		// Token: 0x04002E84 RID: 11908
		internal static readonly RmsTemplate Empty = RmsTemplate.CreateEmptyTemplate();

		// Token: 0x04002E85 RID: 11909
		internal static readonly RmsTemplate DoNotForward = RmsTemplate.CreateDoNotForwardTemplate();

		// Token: 0x04002E86 RID: 11910
		internal static readonly RmsTemplate InternetConfidential = RmsTemplate.CreateInternetConfidentialTemplate();

		// Token: 0x04002E87 RID: 11911
		private readonly Guid id;

		// Token: 0x04002E88 RID: 11912
		private readonly RmsTemplateType type;

		// Token: 0x020009B9 RID: 2489
		[Serializable]
		private sealed class ServerRmsTemplate : RmsTemplate
		{
			// Token: 0x06003622 RID: 13858 RVA: 0x00089CD7 File Offset: 0x00087ED7
			internal ServerRmsTemplate(Guid id, string template, RmsTemplateType templateType = RmsTemplateType.Distributed) : base(id, templateType)
			{
				if (string.IsNullOrEmpty(template))
				{
					throw new ArgumentNullException("template");
				}
				this.template = template;
			}

			// Token: 0x06003623 RID: 13859 RVA: 0x00089CFB File Offset: 0x00087EFB
			internal ServerRmsTemplate(Guid id, string template, ICollection<RmsTemplate.LocaleNameDescription> localeNameDescriptions) : base(id)
			{
				if (string.IsNullOrEmpty(template))
				{
					throw new ArgumentNullException("template");
				}
				this.template = template;
				this.InitializeLocaleNameDescriptionMap(localeNameDescriptions);
			}

			// Token: 0x17000DE5 RID: 3557
			// (get) Token: 0x06003624 RID: 13860 RVA: 0x00089D25 File Offset: 0x00087F25
			public override bool RequiresRepublishingWhenRecipientsChange
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003625 RID: 13861 RVA: 0x00089D28 File Offset: 0x00087F28
			protected override int GetSize()
			{
				if (string.IsNullOrEmpty(this.template))
				{
					return 0;
				}
				return this.template.Length * 2;
			}

			// Token: 0x06003626 RID: 13862 RVA: 0x00089D48 File Offset: 0x00087F48
			protected override SafeRightsManagementPubHandle CreateUnsignedIssuanceLicense(SafeRightsManagementPubHandle ownerHandle, SafeRightsManagementPubHandle fromHandle, IEnumerable<string> recipients)
			{
				SafeRightsManagementPubHandle result;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					SafeRightsManagementPubHandle safeRightsManagementPubHandle;
					int hr = SafeNativeMethods.DRMCreateIssuanceLicense(null, null, null, null, ownerHandle ?? SafeRightsManagementPubHandle.InvalidHandle, this.template, SafeRightsManagementHandle.InvalidHandle, out safeRightsManagementPubHandle);
					disposeGuard.Add<SafeRightsManagementPubHandle>(safeRightsManagementPubHandle);
					Errors.ThrowOnErrorCode(hr);
					disposeGuard.Success();
					result = safeRightsManagementPubHandle;
				}
				return result;
			}

			// Token: 0x06003627 RID: 13863 RVA: 0x00089DB8 File Offset: 0x00087FB8
			protected override void GetNameAndDescription(CultureInfo locale, out string templateName, out string templateDescription)
			{
				if (this.namesAndDescriptions == null)
				{
					this.InitializeLocaleNameDescriptionMap(RmsTemplate.GetTemplateNamesAndDescriptions(this.template));
				}
				templateName = string.Empty;
				templateDescription = string.Empty;
				RmsTemplate.ServerRmsTemplate.NameAndDescriptionPair nameAndDescriptionPair;
				if (locale != null && this.namesAndDescriptions.TryGetValue((uint)locale.LCID, out nameAndDescriptionPair))
				{
					templateName = nameAndDescriptionPair.Name;
					templateDescription = nameAndDescriptionPair.Description;
					return;
				}
				if (locale != null && locale.Parent != null && this.namesAndDescriptions.TryGetValue((uint)locale.Parent.LCID, out nameAndDescriptionPair))
				{
					templateName = nameAndDescriptionPair.Name;
					templateDescription = nameAndDescriptionPair.Description;
					return;
				}
				if (this.namesAndDescriptions.TryGetValue((uint)CultureInfo.InstalledUICulture.LCID, out nameAndDescriptionPair))
				{
					templateName = nameAndDescriptionPair.Name;
					templateDescription = nameAndDescriptionPair.Description;
					return;
				}
				if (this.namesAndDescriptions.TryGetValue((uint)CultureInfo.InstalledUICulture.Parent.LCID, out nameAndDescriptionPair))
				{
					templateName = nameAndDescriptionPair.Name;
					templateDescription = nameAndDescriptionPair.Description;
					return;
				}
				if (this.namesAndDescriptions.TryGetValue((uint)CultureInfo.InvariantCulture.LCID, out nameAndDescriptionPair))
				{
					templateName = nameAndDescriptionPair.Name;
					templateDescription = nameAndDescriptionPair.Description;
				}
			}

			// Token: 0x06003628 RID: 13864 RVA: 0x00089ED8 File Offset: 0x000880D8
			private void InitializeLocaleNameDescriptionMap(ICollection<RmsTemplate.LocaleNameDescription> localeNameDescriptions)
			{
				if (localeNameDescriptions == null)
				{
					throw new ArgumentNullException("localeNameDescriptions");
				}
				Dictionary<uint, RmsTemplate.ServerRmsTemplate.NameAndDescriptionPair> dictionary = new Dictionary<uint, RmsTemplate.ServerRmsTemplate.NameAndDescriptionPair>(localeNameDescriptions.Count);
				bool flag = true;
				foreach (RmsTemplate.LocaleNameDescription localeNameDescription in localeNameDescriptions)
				{
					RmsTemplate.ServerRmsTemplate.NameAndDescriptionPair value = new RmsTemplate.ServerRmsTemplate.NameAndDescriptionPair(localeNameDescription.Name, localeNameDescription.Description);
					dictionary[localeNameDescription.LocaleId] = value;
					if (flag)
					{
						dictionary[(uint)CultureInfo.InvariantCulture.LCID] = value;
						flag = false;
					}
				}
				this.namesAndDescriptions = dictionary;
			}

			// Token: 0x04002E89 RID: 11913
			private readonly string template;

			// Token: 0x04002E8A RID: 11914
			private Dictionary<uint, RmsTemplate.ServerRmsTemplate.NameAndDescriptionPair> namesAndDescriptions;

			// Token: 0x020009BA RID: 2490
			[Serializable]
			private struct NameAndDescriptionPair
			{
				// Token: 0x06003629 RID: 13865 RVA: 0x00089F7C File Offset: 0x0008817C
				public NameAndDescriptionPair(string name, string description)
				{
					this.name = name;
					this.description = description;
				}

				// Token: 0x17000DE6 RID: 3558
				// (get) Token: 0x0600362A RID: 13866 RVA: 0x00089F8C File Offset: 0x0008818C
				public string Name
				{
					get
					{
						return this.name ?? string.Empty;
					}
				}

				// Token: 0x17000DE7 RID: 3559
				// (get) Token: 0x0600362B RID: 13867 RVA: 0x00089F9D File Offset: 0x0008819D
				public string Description
				{
					get
					{
						return this.description ?? string.Empty;
					}
				}

				// Token: 0x04002E8B RID: 11915
				private readonly string name;

				// Token: 0x04002E8C RID: 11916
				private readonly string description;
			}
		}

		// Token: 0x020009BB RID: 2491
		[Serializable]
		private sealed class OneOffRmsTemplate : RmsTemplate
		{
			// Token: 0x0600362C RID: 13868 RVA: 0x00089FAE File Offset: 0x000881AE
			public OneOffRmsTemplate(Guid id, string cultureNeutralName, LocalizedString name, LocalizedString description, ICollection<string> recipientRights) : base(id)
			{
				if (recipientRights == null)
				{
					throw new ArgumentNullException("recipientRights");
				}
				this.cultureNeutralName = cultureNeutralName;
				this.name = name;
				this.description = description;
				this.recipientRights = recipientRights;
			}

			// Token: 0x17000DE8 RID: 3560
			// (get) Token: 0x0600362D RID: 13869 RVA: 0x00089FE4 File Offset: 0x000881E4
			public override bool RequiresRepublishingWhenRecipientsChange
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600362E RID: 13870 RVA: 0x00089FE8 File Offset: 0x000881E8
			protected override int GetSize()
			{
				int num = 0;
				if (!string.IsNullOrEmpty(this.cultureNeutralName))
				{
					num += this.cultureNeutralName.Length * 2;
				}
				if (!this.name.IsEmpty)
				{
					num += this.name.ToString().Length * 2;
				}
				if (!this.description.IsEmpty)
				{
					num += this.description.ToString().Length * 2;
				}
				return num + 4;
			}

			// Token: 0x0600362F RID: 13871 RVA: 0x0008A078 File Offset: 0x00088278
			protected override SafeRightsManagementPubHandle CreateUnsignedIssuanceLicense(SafeRightsManagementPubHandle ownerHandle, SafeRightsManagementPubHandle fromHandle, IEnumerable<string> recipients)
			{
				if (recipients == null)
				{
					throw new ArgumentNullException("recipients");
				}
				SafeRightsManagementPubHandle result;
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					SafeRightsManagementPubHandle safeRightsManagementPubHandle;
					int hr = SafeNativeMethods.DRMCreateIssuanceLicense(null, null, null, null, ownerHandle ?? SafeRightsManagementPubHandle.InvalidHandle, null, SafeRightsManagementHandle.InvalidHandle, out safeRightsManagementPubHandle);
					disposeGuard.Add<SafeRightsManagementPubHandle>(safeRightsManagementPubHandle);
					Errors.ThrowOnErrorCode(hr);
					this.AddRights(safeRightsManagementPubHandle, ownerHandle, fromHandle, recipients);
					this.AddApplicationSpecificRights(safeRightsManagementPubHandle);
					if (!base.Equals(RmsTemplate.DoNotForward))
					{
						hr = SafeNativeMethods.DRMSetNameAndDescription(safeRightsManagementPubHandle, false, 0U, this.cultureNeutralName, base.GetDescription(CultureInfo.InvariantCulture));
						Errors.ThrowOnErrorCode(hr);
						foreach (CultureInfo cultureInfo in RmsTemplate.SupportedClientLanguages)
						{
							string value;
							string value2;
							this.GetNameAndDescription(cultureInfo, out value, out value2);
							if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(value2))
							{
								hr = SafeNativeMethods.DRMSetNameAndDescription(safeRightsManagementPubHandle, false, (uint)cultureInfo.LCID, value, value2);
								Errors.ThrowOnErrorCode(hr);
							}
						}
					}
					disposeGuard.Success();
					result = safeRightsManagementPubHandle;
				}
				return result;
			}

			// Token: 0x06003630 RID: 13872 RVA: 0x0008A1A4 File Offset: 0x000883A4
			protected override void GetNameAndDescription(CultureInfo locale, out string templateName, out string templateDescription)
			{
				templateName = this.name.ToString(locale);
				templateDescription = this.description.ToString(locale);
			}

			// Token: 0x06003631 RID: 13873 RVA: 0x0008A1D3 File Offset: 0x000883D3
			private void AddRights(SafeRightsManagementPubHandle issuanceLicenseHandle, SafeRightsManagementPubHandle ownerHandle, SafeRightsManagementPubHandle fromHandle, IEnumerable<string> recipients)
			{
				this.AddRecipientRights(issuanceLicenseHandle, recipients);
				if (ownerHandle != null && !ownerHandle.IsInvalid)
				{
					this.AddOwnerRights(issuanceLicenseHandle, ownerHandle);
				}
				if (fromHandle != null && !fromHandle.IsInvalid)
				{
					this.AddOwnerRights(issuanceLicenseHandle, fromHandle);
				}
			}

			// Token: 0x06003632 RID: 13874 RVA: 0x0008A204 File Offset: 0x00088404
			private void AddOwnerRights(SafeRightsManagementPubHandle issuanceLicenseHandle, SafeRightsManagementPubHandle ownerHandle)
			{
				SafeRightsManagementPubHandle safeRightsManagementPubHandle = null;
				try
				{
					int hr = SafeNativeMethods.DRMCreateRight("OWNER", null, null, 0U, null, null, out safeRightsManagementPubHandle);
					Errors.ThrowOnErrorCode(hr);
					hr = SafeNativeMethods.DRMAddRightWithUser(issuanceLicenseHandle, safeRightsManagementPubHandle, ownerHandle);
					Errors.ThrowOnErrorCode(hr);
				}
				finally
				{
					if (safeRightsManagementPubHandle != null)
					{
						safeRightsManagementPubHandle.Close();
					}
				}
			}

			// Token: 0x06003633 RID: 13875 RVA: 0x0008A258 File Offset: 0x00088458
			private void AddRecipientRights(SafeRightsManagementPubHandle issuanceLicenseHandle, IEnumerable<string> recipients)
			{
				SafeRightsManagementPubHandle[] array = new SafeRightsManagementPubHandle[this.recipientRights.Count];
				try
				{
					int num = 0;
					foreach (string rightName in this.recipientRights)
					{
						int hr = SafeNativeMethods.DRMCreateRight(rightName, null, null, 0U, null, null, out array[num++]);
						Errors.ThrowOnErrorCode(hr);
					}
					foreach (string userName in recipients)
					{
						SafeRightsManagementPubHandle safeRightsManagementPubHandle = null;
						try
						{
							int hr2 = SafeNativeMethods.DRMCreateUser(userName, null, "Unspecified", out safeRightsManagementPubHandle);
							Errors.ThrowOnErrorCode(hr2);
							foreach (SafeRightsManagementPubHandle rightHandle in array)
							{
								hr2 = SafeNativeMethods.DRMAddRightWithUser(issuanceLicenseHandle, rightHandle, safeRightsManagementPubHandle);
								Errors.ThrowOnErrorCode(hr2);
							}
						}
						finally
						{
							if (safeRightsManagementPubHandle != null)
							{
								safeRightsManagementPubHandle.Close();
							}
						}
					}
				}
				finally
				{
					foreach (SafeRightsManagementPubHandle safeRightsManagementPubHandle2 in array)
					{
						if (safeRightsManagementPubHandle2 != null)
						{
							safeRightsManagementPubHandle2.Close();
						}
					}
				}
			}

			// Token: 0x06003634 RID: 13876 RVA: 0x0008A3A8 File Offset: 0x000885A8
			private void AddApplicationSpecificRights(SafeRightsManagementPubHandle issuanceLicenseHandle)
			{
				int hr = SafeNativeMethods.DRMSetApplicationSpecificData(issuanceLicenseHandle, false, "ExchangeRecipientOrganizationExtractAllowed", "true");
				Errors.ThrowOnErrorCode(hr);
			}

			// Token: 0x04002E8D RID: 11917
			private readonly string cultureNeutralName;

			// Token: 0x04002E8E RID: 11918
			private readonly LocalizedString name;

			// Token: 0x04002E8F RID: 11919
			private readonly LocalizedString description;

			// Token: 0x04002E90 RID: 11920
			private readonly ICollection<string> recipientRights;
		}

		// Token: 0x020009BC RID: 2492
		[Serializable]
		private struct LocaleNameDescription
		{
			// Token: 0x06003635 RID: 13877 RVA: 0x0008A3CD File Offset: 0x000885CD
			internal LocaleNameDescription(uint localeId, string name, string description)
			{
				this.LocaleId = localeId;
				this.Name = name;
				this.Description = description;
			}

			// Token: 0x04002E91 RID: 11921
			internal readonly uint LocaleId;

			// Token: 0x04002E92 RID: 11922
			internal readonly string Name;

			// Token: 0x04002E93 RID: 11923
			internal readonly string Description;
		}
	}
}
