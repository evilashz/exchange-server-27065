using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200007A RID: 122
	[Cmdlet("New", "ActiveSyncDeviceAccessRule", SupportsShouldProcess = true)]
	public sealed class NewActiveSyncDeviceAccessRule : NewMultitenancyFixedNameSystemConfigurationObjectTask<ActiveSyncDeviceAccessRule>
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000FDA7 File Offset: 0x0000DFA7
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		[Parameter(Mandatory = true)]
		public DeviceAccessLevel AccessLevel
		{
			get
			{
				return this.DataObject.AccessLevel;
			}
			set
			{
				this.DataObject.AccessLevel = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000FDC2 File Offset: 0x0000DFC2
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000FDCF File Offset: 0x0000DFCF
		[Parameter(Mandatory = true)]
		public DeviceAccessCharacteristic Characteristic
		{
			get
			{
				return this.DataObject.Characteristic;
			}
			set
			{
				this.DataObject.Characteristic = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000FDDD File Offset: 0x0000DFDD
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000FDEA File Offset: 0x0000DFEA
		[Parameter(Mandatory = true)]
		public string QueryString
		{
			get
			{
				return this.DataObject.QueryString;
			}
			set
			{
				this.DataObject.QueryString = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewActiveSyncDeviceAccessRule(this.BuildRuleName());
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000FE08 File Offset: 0x0000E008
		protected override IConfigurable PrepareDataObject()
		{
			this.DataObject.Name = this.BuildRuleName();
			ActiveSyncDeviceAccessRule activeSyncDeviceAccessRule = (ActiveSyncDeviceAccessRule)base.PrepareDataObject();
			activeSyncDeviceAccessRule.SetId((IConfigurationSession)base.DataSession, this.DataObject.Name);
			return activeSyncDeviceAccessRule;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000FE50 File Offset: 0x0000E050
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (Datacenter.IsMultiTenancyEnabled() && this.DataObject.OrganizationId == OrganizationId.ForestWideOrgId && this.AccessLevel != DeviceAccessLevel.Block)
			{
				base.WriteError(new ArgumentException(Strings.ErrorOnlyForestWideBlockIsAllowed), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		private string BuildRuleName()
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", new object[]
			{
				this.QueryString,
				this.Characteristic
			});
			if (text.Length > 64)
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", new object[]
				{
					text.GetHashCode().ToString(),
					this.Characteristic
				});
				text = text.Substring(0, 64 - text2.Length) + text2;
			}
			return text;
		}

		// Token: 0x04000211 RID: 529
		private const int MaxRuleNameLength = 64;
	}
}
