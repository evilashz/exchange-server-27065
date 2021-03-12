using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public class ScopeSettings : ExchangeDataObject
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000B9E3 File Offset: 0x00009BE3
		public ScopeSettings()
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000B9EB File Offset: 0x00009BEB
		internal ScopeSettings(ExchangeADServerSettings settings)
		{
			base.PropertyBag.Clear();
			if (settings != null)
			{
				this.ForestViewEnabled = settings.ForestViewEnabled;
				this.OrganizationalUnit = settings.OrganizationalUnit;
			}
			else
			{
				this.ForestViewEnabled = true;
			}
			base.ResetChangeTracking();
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000BA28 File Offset: 0x00009C28
		internal override ObjectSchema Schema
		{
			get
			{
				return ScopeSettings.schema;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000BA2F File Offset: 0x00009C2F
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000BA4B File Offset: 0x00009C4B
		public bool ForestViewEnabled
		{
			get
			{
				return (bool)(this[ScopeSettingsSchema.ForestViewEnabled] ?? false);
			}
			set
			{
				this[ScopeSettingsSchema.ForestViewEnabled] = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000BA5E File Offset: 0x00009C5E
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000BA69 File Offset: 0x00009C69
		public bool DomainViewEnabled
		{
			get
			{
				return !this.ForestViewEnabled;
			}
			set
			{
				this.ForestViewEnabled = !value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000BA75 File Offset: 0x00009C75
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000BA87 File Offset: 0x00009C87
		public virtual string OrganizationalUnit
		{
			get
			{
				return (string)this[ScopeSettingsSchema.OrganizationalUnit];
			}
			set
			{
				this[ScopeSettingsSchema.OrganizationalUnit] = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000BA95 File Offset: 0x00009C95
		public string ScopingDescription
		{
			get
			{
				if (!this.ForestViewEnabled)
				{
					return this.OrganizationalUnit;
				}
				return Strings.EntireForest;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		public override ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			if (this.DomainViewEnabled && string.IsNullOrEmpty(this.OrganizationalUnit))
			{
				list.Add(new PropertyValidationError(Strings.ErrorOrganizationalUnitEmpty, new ADPropertyDefinition("OrganizationalUnit", ExchangeObjectVersion.Exchange2003, typeof(string), "Dummy Property", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null), this));
			}
			return list.ToArray();
		}

		// Token: 0x040000DD RID: 221
		private static ScopeSettingsSchema schema = ObjectSchema.GetInstance<ScopeSettingsSchema>();
	}
}
