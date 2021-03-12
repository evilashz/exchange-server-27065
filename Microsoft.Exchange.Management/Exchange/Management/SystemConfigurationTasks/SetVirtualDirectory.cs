using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C41 RID: 3137
	public abstract class SetVirtualDirectory<T> : SetTopologySystemConfigurationObjectTask<VirtualDirectoryIdParameter, T> where T : ADVirtualDirectory, new()
	{
		// Token: 0x17002485 RID: 9349
		// (get) Token: 0x06007687 RID: 30343 RVA: 0x001E3C08 File Offset: 0x001E1E08
		// (set) Token: 0x06007688 RID: 30344 RVA: 0x001E3C1F File Offset: 0x001E1E1F
		[Parameter]
		public Uri InternalUrl
		{
			get
			{
				return (Uri)base.Fields[SetVirtualDirectory<T>.InternalUrlKey];
			}
			set
			{
				base.Fields[SetVirtualDirectory<T>.InternalUrlKey] = value;
			}
		}

		// Token: 0x17002486 RID: 9350
		// (get) Token: 0x06007689 RID: 30345 RVA: 0x001E3C32 File Offset: 0x001E1E32
		// (set) Token: 0x0600768A RID: 30346 RVA: 0x001E3C49 File Offset: 0x001E1E49
		[Parameter]
		public MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)base.Fields[SetVirtualDirectory<T>.InternalAuthenticationMethodsKey];
			}
			set
			{
				base.Fields[SetVirtualDirectory<T>.InternalAuthenticationMethodsKey] = value;
			}
		}

		// Token: 0x17002487 RID: 9351
		// (get) Token: 0x0600768B RID: 30347 RVA: 0x001E3C5C File Offset: 0x001E1E5C
		// (set) Token: 0x0600768C RID: 30348 RVA: 0x001E3C73 File Offset: 0x001E1E73
		[Parameter]
		public Uri ExternalUrl
		{
			get
			{
				return (Uri)base.Fields[SetVirtualDirectory<T>.ExternalUrlKey];
			}
			set
			{
				base.Fields[SetVirtualDirectory<T>.ExternalUrlKey] = value;
			}
		}

		// Token: 0x17002488 RID: 9352
		// (get) Token: 0x0600768D RID: 30349 RVA: 0x001E3C86 File Offset: 0x001E1E86
		// (set) Token: 0x0600768E RID: 30350 RVA: 0x001E3C9D File Offset: 0x001E1E9D
		[Parameter]
		public MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)base.Fields[SetVirtualDirectory<T>.ExternalAuthenticationMethodsKey];
			}
			set
			{
				base.Fields[SetVirtualDirectory<T>.ExternalAuthenticationMethodsKey] = value;
			}
		}

		// Token: 0x0600768F RID: 30351 RVA: 0x001E3CB0 File Offset: 0x001E1EB0
		protected override IConfigurable PrepareDataObject()
		{
			ADVirtualDirectory advirtualDirectory = (ADVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields.Contains(SetVirtualDirectory<T>.InternalUrlKey))
			{
				advirtualDirectory.InternalUrl = (Uri)base.Fields[SetVirtualDirectory<T>.InternalUrlKey];
			}
			if (base.Fields.Contains(SetVirtualDirectory<T>.InternalAuthenticationMethodsKey))
			{
				advirtualDirectory.InternalAuthenticationMethods = (MultiValuedProperty<AuthenticationMethod>)base.Fields[SetVirtualDirectory<T>.InternalAuthenticationMethodsKey];
			}
			if (base.Fields.Contains(SetVirtualDirectory<T>.ExternalUrlKey))
			{
				advirtualDirectory.ExternalUrl = (Uri)base.Fields[SetVirtualDirectory<T>.ExternalUrlKey];
			}
			if (base.Fields.Contains(SetVirtualDirectory<T>.ExternalAuthenticationMethodsKey))
			{
				advirtualDirectory.ExternalAuthenticationMethods = (MultiValuedProperty<AuthenticationMethod>)base.Fields[SetVirtualDirectory<T>.ExternalAuthenticationMethodsKey];
			}
			return advirtualDirectory;
		}

		// Token: 0x04003BC5 RID: 15301
		protected static readonly string InternalUrlKey = "InternalUrl";

		// Token: 0x04003BC6 RID: 15302
		protected static readonly string InternalAuthenticationMethodsKey = "InternalAuthenticationMethods";

		// Token: 0x04003BC7 RID: 15303
		protected static readonly string ExternalUrlKey = "ExternalUrl";

		// Token: 0x04003BC8 RID: 15304
		protected static readonly string ExternalAuthenticationMethodsKey = "ExternalAuthenticationMethods";
	}
}
