using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009AD RID: 2477
	[Cmdlet("Get", "DomainController", DefaultParameterSetName = "DomainController")]
	public sealed class GetDomainController : GetTaskBase<ADServer>
	{
		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x0600586B RID: 22635 RVA: 0x00170C29 File Offset: 0x0016EE29
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x0600586C RID: 22636 RVA: 0x00170C2C File Offset: 0x0016EE2C
		// (set) Token: 0x0600586D RID: 22637 RVA: 0x00170C52 File Offset: 0x0016EE52
		[Parameter(Mandatory = false, ParameterSetName = "GlobalCatalog")]
		public SwitchParameter GlobalCatalog
		{
			get
			{
				return (SwitchParameter)(base.Fields["GlobalCatalog"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["GlobalCatalog"] = value;
			}
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x0600586E RID: 22638 RVA: 0x00170C6A File Offset: 0x0016EE6A
		// (set) Token: 0x0600586F RID: 22639 RVA: 0x00170C81 File Offset: 0x0016EE81
		[Parameter(Mandatory = false, ParameterSetName = "DomainController")]
		public Fqdn DomainName
		{
			get
			{
				return (Fqdn)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06005870 RID: 22640 RVA: 0x00170C94 File Offset: 0x0016EE94
		// (set) Token: 0x06005871 RID: 22641 RVA: 0x00170CAB File Offset: 0x0016EEAB
		[Parameter(Mandatory = false, ParameterSetName = "GlobalCatalog")]
		public Fqdn Forest
		{
			get
			{
				return (Fqdn)base.Fields["Forest"];
			}
			set
			{
				base.Fields["Forest"] = value;
			}
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x06005872 RID: 22642 RVA: 0x00170CBE File Offset: 0x0016EEBE
		// (set) Token: 0x06005873 RID: 22643 RVA: 0x00170CD5 File Offset: 0x0016EED5
		[Parameter]
		public NetworkCredential Credential
		{
			get
			{
				return (NetworkCredential)base.Fields["Credential"];
			}
			set
			{
				base.Fields["Credential"] = value;
			}
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x00170CE8 File Offset: 0x0016EEE8
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 84, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\ExchangeServer\\GetDomainController.cs");
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x00170D06 File Offset: 0x0016EF06
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(NotSupportedException).IsInstanceOfType(exception);
		}

		// Token: 0x06005876 RID: 22646 RVA: 0x00170D28 File Offset: 0x0016EF28
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ADDomainController dataObject2 = new ADDomainController((ADServer)dataObject);
			base.WriteResult(dataObject2);
		}

		// Token: 0x06005877 RID: 22647 RVA: 0x00170D62 File Offset: 0x0016EF62
		protected override void InternalBeginProcessing()
		{
			if (this.Credential != null && this.DomainName == null && this.Forest == null)
			{
				base.WriteError(new ArgumentException(Strings.CannotOnlySpecifyCredential), ErrorCategory.InvalidArgument, this);
			}
			base.InternalBeginProcessing();
		}

		// Token: 0x06005878 RID: 22648 RVA: 0x00170D9C File Offset: 0x0016EF9C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADForest adforest;
			if (this.Forest == null)
			{
				adforest = ADForest.GetLocalForest();
			}
			else
			{
				adforest = ADForest.GetForest(this.Forest, this.Credential);
			}
			List<ADServer> list = new List<ADServer>();
			if (this.GlobalCatalog)
			{
				list.AddRange(adforest.FindAllGlobalCatalogs(false));
			}
			else
			{
				if (this.DomainName == null)
				{
					using (ReadOnlyCollection<ADDomain>.Enumerator enumerator = adforest.FindDomains().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ADDomain addomain = enumerator.Current;
							list.AddRange(addomain.FindAllDomainControllers(false));
						}
						goto IL_F3;
					}
				}
				ADDomain addomain2;
				if (this.Credential == null)
				{
					addomain2 = adforest.FindDomainByFqdn(this.DomainName.ToString());
				}
				else
				{
					addomain2 = ADForest.FindExternalDomain(this.DomainName.ToString(), this.Credential);
				}
				if (addomain2 != null)
				{
					list.AddRange(addomain2.FindAllDomainControllers(false));
				}
				else
				{
					base.WriteError(new DomainNotFoundException(this.DomainName.ToString()), ErrorCategory.InvalidArgument, null);
				}
			}
			IL_F3:
			this.WriteResult<ADServer>(list);
			TaskLogger.LogExit();
		}
	}
}
