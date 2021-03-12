using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C01 RID: 3073
	public abstract class NewVirtualDirectory<T> : NewFixedNameSystemConfigurationObjectTask<T> where T : ADVirtualDirectory, new()
	{
		// Token: 0x170023A6 RID: 9126
		// (get) Token: 0x060073E0 RID: 29664 RVA: 0x001D8062 File Offset: 0x001D6262
		protected Server OwningServer
		{
			get
			{
				return this.owningServer;
			}
		}

		// Token: 0x170023A7 RID: 9127
		// (get) Token: 0x060073E1 RID: 29665 RVA: 0x001D806A File Offset: 0x001D626A
		// (set) Token: 0x060073E2 RID: 29666 RVA: 0x001D8081 File Offset: 0x001D6281
		[Parameter(ValueFromPipeline = true)]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x170023A8 RID: 9128
		// (get) Token: 0x060073E3 RID: 29667 RVA: 0x001D8094 File Offset: 0x001D6294
		// (set) Token: 0x060073E4 RID: 29668 RVA: 0x001D80AB File Offset: 0x001D62AB
		[Parameter(Mandatory = false, Position = 0)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x170023A9 RID: 9129
		// (get) Token: 0x060073E5 RID: 29669 RVA: 0x001D80C0 File Offset: 0x001D62C0
		// (set) Token: 0x060073E6 RID: 29670 RVA: 0x001D80E4 File Offset: 0x001D62E4
		[Parameter]
		public Uri InternalUrl
		{
			get
			{
				T dataObject = this.DataObject;
				return dataObject.InternalUrl;
			}
			set
			{
				T dataObject = this.DataObject;
				dataObject.InternalUrl = value;
			}
		}

		// Token: 0x170023AA RID: 9130
		// (get) Token: 0x060073E7 RID: 29671 RVA: 0x001D8108 File Offset: 0x001D6308
		// (set) Token: 0x060073E8 RID: 29672 RVA: 0x001D812C File Offset: 0x001D632C
		[Parameter]
		public MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				T dataObject = this.DataObject;
				return dataObject.InternalAuthenticationMethods;
			}
			set
			{
				T dataObject = this.DataObject;
				dataObject.InternalAuthenticationMethods = value;
			}
		}

		// Token: 0x170023AB RID: 9131
		// (get) Token: 0x060073E9 RID: 29673 RVA: 0x001D8150 File Offset: 0x001D6350
		// (set) Token: 0x060073EA RID: 29674 RVA: 0x001D8174 File Offset: 0x001D6374
		[Parameter]
		public Uri ExternalUrl
		{
			get
			{
				T dataObject = this.DataObject;
				return dataObject.ExternalUrl;
			}
			set
			{
				T dataObject = this.DataObject;
				dataObject.ExternalUrl = value;
			}
		}

		// Token: 0x170023AC RID: 9132
		// (get) Token: 0x060073EB RID: 29675 RVA: 0x001D8198 File Offset: 0x001D6398
		// (set) Token: 0x060073EC RID: 29676 RVA: 0x001D81BC File Offset: 0x001D63BC
		[Parameter]
		public MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				T dataObject = this.DataObject;
				return dataObject.ExternalAuthenticationMethods;
			}
			set
			{
				T dataObject = this.DataObject;
				dataObject.ExternalAuthenticationMethods = value;
			}
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x001D81E0 File Offset: 0x001D63E0
		protected override IConfigurable PrepareDataObject()
		{
			ADVirtualDirectory advirtualDirectory = (ADVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (!this.ShouldCreateVirtualDirectory())
			{
				base.WriteError(this.owningServer.GetServerRoleError(ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport), ErrorCategory.InvalidOperation, this.Server);
				return null;
			}
			advirtualDirectory.SetId(this.owningServer.Id.GetChildId("Protocols").GetChildId("HTTP").GetChildId(this.Name));
			return advirtualDirectory;
		}

		// Token: 0x060073EE RID: 29678 RVA: 0x001D8258 File Offset: 0x001D6458
		protected virtual bool ShouldCreateVirtualDirectory()
		{
			if (this.Server == null)
			{
				this.Server = new ServerIdParameter();
			}
			this.owningServer = (Server)base.GetDataObject<Server>(this.Server, base.DataSession, this.RootId, new LocalizedString?(Strings.ErrorServerNotFound(this.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(this.Server.ToString())));
			return !base.HasErrors && (this.OwningServer.IsClientAccessServer || this.OwningServer.IsCafeServer);
		}

		// Token: 0x04003AEA RID: 15082
		private Server owningServer;
	}
}
