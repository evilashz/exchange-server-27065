using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000152 RID: 338
	public abstract class ProxyAddressBaseDataHandler : ExchangeDataHandler
	{
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00035021 File Offset: 0x00033221
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x00035029 File Offset: 0x00033229
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(Strings.ProxyAddressTypeEmptyError);
				}
				ProxyAddressPrefix.GetPrefix(value);
				this.prefix = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00035051 File Offset: 0x00033251
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x00035059 File Offset: 0x00033259
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				ProxyAddressBase.ValidateAddressString(value);
				this.address = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00035068 File Offset: 0x00033268
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x00035070 File Offset: 0x00033270
		public virtual ProxyAddressBase ProxyAddressBase
		{
			get
			{
				return this.proxyAddressBase;
			}
			set
			{
				this.proxyAddressBase = value;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00035079 File Offset: 0x00033279
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00035081 File Offset: 0x00033281
		public ProxyAddressBase OriginalProxyAddressBase
		{
			get
			{
				return this.originalProxyAddressBase;
			}
			set
			{
				this.originalProxyAddressBase = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0003508A File Offset: 0x0003328A
		public List<ProxyAddressBase> ProxyAddresses
		{
			get
			{
				return this.proxyAddresses;
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00035092 File Offset: 0x00033292
		public ProxyAddressBaseDataHandler()
		{
			base.DataSource = this;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x000350AC File Offset: 0x000332AC
		public override ValidationError[] Validate()
		{
			ValidationError[] collection = base.Validate();
			List<ValidationError> list = new List<ValidationError>(collection);
			PropertyDefinition propertyDefinition = new AdminPropertyDefinition(this.BindingProperty, ExchangeObjectVersion.Exchange2003, typeof(string), null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
			try
			{
				this.ProxyAddressBase = this.GetValidatedProxyAddressBase(this.Prefix, this.Address);
				if (this.ProxyAddressBase != this.OriginalProxyAddressBase && this.ProxyAddresses.Contains(this.ProxyAddressBase))
				{
					list.Add(new PropertyValidationError(Strings.ProxyAddressExistedError, propertyDefinition, null));
				}
			}
			catch (ArgumentException ex)
			{
				list.Add(new PropertyValidationError(new LocalizedString(ex.Message), propertyDefinition, null));
			}
			return list.ToArray();
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00035170 File Offset: 0x00033370
		protected virtual string BindingProperty
		{
			get
			{
				return "Address";
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00035178 File Offset: 0x00033378
		private ProxyAddressBase GetValidatedProxyAddressBase(string prefix, string address)
		{
			ProxyAddressBase proxyAddressBase = this.InternalGetProxyAddressBase(prefix, address);
			IInvalidProxy invalidProxy = proxyAddressBase as IInvalidProxy;
			if (invalidProxy != null)
			{
				throw invalidProxy.ParseException;
			}
			return proxyAddressBase;
		}

		// Token: 0x06000DEF RID: 3567
		protected abstract ProxyAddressBase InternalGetProxyAddressBase(string prefix, string address);

		// Token: 0x0400058A RID: 1418
		private string prefix;

		// Token: 0x0400058B RID: 1419
		private string address;

		// Token: 0x0400058C RID: 1420
		private ProxyAddressBase proxyAddressBase;

		// Token: 0x0400058D RID: 1421
		private ProxyAddressBase originalProxyAddressBase;

		// Token: 0x0400058E RID: 1422
		private List<ProxyAddressBase> proxyAddresses = new List<ProxyAddressBase>();
	}
}
