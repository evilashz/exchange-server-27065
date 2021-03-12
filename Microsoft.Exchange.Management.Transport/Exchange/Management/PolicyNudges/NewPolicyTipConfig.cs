using System;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02000061 RID: 97
	[Cmdlet("New", "PolicyTipConfig", DefaultParameterSetName = "Paramters", SupportsShouldProcess = true)]
	public sealed class NewPolicyTipConfig : NewMultitenancySystemConfigurationObjectTask<PolicyTipMessageConfig>
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000D328 File Offset: 0x0000B528
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000D33F File Offset: 0x0000B53F
		[Parameter(Mandatory = true)]
		public string Value
		{
			get
			{
				return (string)base.Fields["Value"];
			}
			set
			{
				base.Fields["Value"] = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000D352 File Offset: 0x0000B552
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewPolicyTipConfig(base.Name, this.Value);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000D368 File Offset: 0x0000B568
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			PolicyTipMessageConfig policyTipMessageConfig = (PolicyTipMessageConfig)base.PrepareDataObject();
			if (this.action != PolicyTipMessageConfigAction.Url)
			{
				policyTipMessageConfig.SetId((IConfigurationSession)base.DataSession, this.locale.Name + "\\" + this.action.ToString());
			}
			else
			{
				policyTipMessageConfig.SetId((IConfigurationSession)base.DataSession, this.action.ToString());
			}
			TaskLogger.LogExit();
			return policyTipMessageConfig;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (base.Name == null)
			{
				base.WriteError(new NewPolicyTipConfigInvalidNameException(NewPolicyTipConfig.supportedLocalesString.Value, NewPolicyTipConfig.supportedActionsString.Value), ErrorCategory.InvalidArgument, null);
			}
			string[] array = base.Name.Split(new char[]
			{
				'\\'
			});
			if (array[0] == PolicyTipMessageConfigAction.Url.ToString())
			{
				if (array.Take(2).Count<string>() > 1)
				{
					base.WriteError(new NewPolicyTipConfigInvalidNameException(NewPolicyTipConfig.supportedLocalesString.Value, NewPolicyTipConfig.supportedActionsString.Value), ErrorCategory.InvalidArgument, null);
				}
				if (!NewPolicyTipConfig.IsAbsoluteUri(this.Value))
				{
					base.WriteError(new NewPolicyTipConfigInvalidUrlException(), ErrorCategory.InvalidArgument, null);
				}
				this.locale = null;
				this.action = PolicyTipMessageConfigAction.Url;
			}
			else
			{
				if (array.Take(2).Count<string>() != 2)
				{
					base.WriteError(new NewPolicyTipConfigInvalidNameException(NewPolicyTipConfig.supportedLocalesString.Value, NewPolicyTipConfig.supportedActionsString.Value), ErrorCategory.InvalidArgument, null);
				}
				try
				{
					this.locale = new CultureInfo(array[0]);
				}
				catch (CultureNotFoundException)
				{
					base.WriteError(new NewPolicyTipConfigInvalidNameException(NewPolicyTipConfig.supportedLocalesString.Value, NewPolicyTipConfig.supportedActionsString.Value), ErrorCategory.InvalidArgument, null);
				}
				if (!LanguagePackInfo.expectedCultureLcids.Contains(this.locale.LCID))
				{
					base.WriteError(new NewPolicyTipConfigInvalidNameException(NewPolicyTipConfig.supportedLocalesString.Value, NewPolicyTipConfig.supportedActionsString.Value), ErrorCategory.InvalidArgument, null);
				}
				if (!Enum.TryParse<PolicyTipMessageConfigAction>(array[1], true, out this.action) || this.action == PolicyTipMessageConfigAction.Url)
				{
					base.WriteError(new NewPolicyTipConfigInvalidNameException(NewPolicyTipConfig.supportedLocalesString.Value, NewPolicyTipConfig.supportedActionsString.Value), ErrorCategory.InvalidArgument, null);
				}
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.action != PolicyTipMessageConfigAction.Url)
			{
				this.DataObject.Action = this.action;
				this.DataObject.Locale = this.locale.Name;
				this.DataObject.Value = this.Value;
			}
			else
			{
				this.DataObject.Action = this.action;
				this.DataObject.Locale = string.Empty;
				this.DataObject.Value = this.Value;
			}
			base.CreateParentContainerIfNeeded(this.DataObject);
			try
			{
				base.InternalProcessRecord();
			}
			catch (ADObjectAlreadyExistsException innerException)
			{
				base.WriteError(new NewPolicyTipConfigDuplicateException(innerException), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000D66C File Offset: 0x0000B86C
		internal static bool IsAbsoluteUri(string uri)
		{
			return new Regex("^https{0,1}://", RegexOptions.IgnoreCase | RegexOptions.ECMAScript).IsMatch(uri);
		}

		// Token: 0x0400013D RID: 317
		private CultureInfo locale;

		// Token: 0x0400013E RID: 318
		private PolicyTipMessageConfigAction action;

		// Token: 0x0400013F RID: 319
		private static Lazy<string> supportedLocalesString = new Lazy<string>(() => string.Join(", ", from lcid in LanguagePackInfo.expectedCultureLcids
		select new CultureInfo(lcid).Name));

		// Token: 0x04000140 RID: 320
		private static Lazy<string> supportedActionsString = new Lazy<string>(() => string.Join(", ", Enum.GetNames(typeof(PolicyTipMessageConfigAction))));
	}
}
