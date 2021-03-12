using System;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security.Policy
{
	// Token: 0x02000316 RID: 790
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class ApplicationSecurityInfo
	{
		// Token: 0x0600285E RID: 10334 RVA: 0x00094790 File Offset: 0x00092990
		internal ApplicationSecurityInfo()
		{
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x00094798 File Offset: 0x00092998
		public ApplicationSecurityInfo(ActivationContext activationContext)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this.m_context = activationContext;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x000947B8 File Offset: 0x000929B8
		// (set) Token: 0x06002861 RID: 10337 RVA: 0x00094801 File Offset: 0x00092A01
		public ApplicationId ApplicationId
		{
			get
			{
				if (this.m_appId == null && this.m_context != null)
				{
					ICMS applicationComponentManifest = this.m_context.ApplicationComponentManifest;
					ApplicationId value = ApplicationSecurityInfo.ParseApplicationId(applicationComponentManifest);
					Interlocked.CompareExchange(ref this.m_appId, value, null);
				}
				return this.m_appId as ApplicationId;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_appId = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x00094818 File Offset: 0x00092A18
		// (set) Token: 0x06002863 RID: 10339 RVA: 0x00094861 File Offset: 0x00092A61
		public ApplicationId DeploymentId
		{
			get
			{
				if (this.m_deployId == null && this.m_context != null)
				{
					ICMS deploymentComponentManifest = this.m_context.DeploymentComponentManifest;
					ApplicationId value = ApplicationSecurityInfo.ParseApplicationId(deploymentComponentManifest);
					Interlocked.CompareExchange(ref this.m_deployId, value, null);
				}
				return this.m_deployId as ApplicationId;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_deployId = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x00094878 File Offset: 0x00092A78
		// (set) Token: 0x06002865 RID: 10341 RVA: 0x00094A2C File Offset: 0x00092C2C
		public PermissionSet DefaultRequestSet
		{
			get
			{
				if (this.m_defaultRequest == null)
				{
					PermissionSet value = new PermissionSet(PermissionState.None);
					if (this.m_context != null)
					{
						ICMS applicationComponentManifest = this.m_context.ApplicationComponentManifest;
						string defaultPermissionSetID = ((IMetadataSectionEntry)applicationComponentManifest.MetadataSectionEntry).defaultPermissionSetID;
						object obj = null;
						if (defaultPermissionSetID != null && defaultPermissionSetID.Length > 0)
						{
							((ISectionWithStringKey)applicationComponentManifest.PermissionSetSection).Lookup(defaultPermissionSetID, out obj);
							IPermissionSetEntry permissionSetEntry = obj as IPermissionSetEntry;
							if (permissionSetEntry != null)
							{
								SecurityElement securityElement = SecurityElement.FromString(permissionSetEntry.AllData.XmlSegment);
								string text = securityElement.Attribute("temp:Unrestricted");
								if (text != null)
								{
									securityElement.AddAttribute("Unrestricted", text);
								}
								string strA = securityElement.Attribute("SameSite");
								if (string.Compare(strA, "Site", StringComparison.OrdinalIgnoreCase) == 0)
								{
									Url url = new Url(this.m_context.Identity.CodeBase);
									URLString urlstring = url.GetURLString();
									NetCodeGroup netCodeGroup = new NetCodeGroup(new AllMembershipCondition());
									SecurityElement securityElement2 = netCodeGroup.CreateWebPermission(urlstring.Host, urlstring.Scheme, urlstring.Port, "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
									if (securityElement2 != null)
									{
										securityElement.AddChild(securityElement2);
									}
									if (string.Compare("file:", 0, this.m_context.Identity.CodeBase, 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
									{
										FileCodeGroup fileCodeGroup = new FileCodeGroup(new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery);
										PolicyStatement policyStatement = fileCodeGroup.CalculatePolicy(url);
										if (policyStatement != null)
										{
											PermissionSet permissionSet = policyStatement.PermissionSet;
											if (permissionSet != null)
											{
												securityElement.AddChild(permissionSet.GetPermission(typeof(FileIOPermission)).ToXml());
											}
										}
									}
								}
								value = new ReadOnlyPermissionSet(securityElement);
							}
						}
					}
					Interlocked.CompareExchange(ref this.m_defaultRequest, value, null);
				}
				return this.m_defaultRequest as PermissionSet;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_defaultRequest = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002866 RID: 10342 RVA: 0x00094A44 File Offset: 0x00092C44
		// (set) Token: 0x06002867 RID: 10343 RVA: 0x00094B41 File Offset: 0x00092D41
		public Evidence ApplicationEvidence
		{
			get
			{
				if (this.m_appEvidence == null)
				{
					Evidence evidence = new Evidence();
					if (this.m_context != null)
					{
						evidence = new Evidence();
						Url evidence2 = new Url(this.m_context.Identity.CodeBase);
						evidence.AddHostEvidence<Url>(evidence2);
						evidence.AddHostEvidence<Zone>(Zone.CreateFromUrl(this.m_context.Identity.CodeBase));
						if (string.Compare("file:", 0, this.m_context.Identity.CodeBase, 0, 5, StringComparison.OrdinalIgnoreCase) != 0)
						{
							evidence.AddHostEvidence<Site>(Site.CreateFromUrl(this.m_context.Identity.CodeBase));
						}
						evidence.AddHostEvidence<StrongName>(new StrongName(new StrongNamePublicKeyBlob(this.DeploymentId.m_publicKeyToken), this.DeploymentId.Name, this.DeploymentId.Version));
						evidence.AddHostEvidence<ActivationArguments>(new ActivationArguments(this.m_context));
					}
					Interlocked.CompareExchange(ref this.m_appEvidence, evidence, null);
				}
				return this.m_appEvidence as Evidence;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_appEvidence = value;
			}
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x00094B58 File Offset: 0x00092D58
		private static ApplicationId ParseApplicationId(ICMS manifest)
		{
			if (manifest.Identity == null)
			{
				return null;
			}
			return new ApplicationId(Hex.DecodeHexString(manifest.Identity.GetAttribute("", "publicKeyToken")), manifest.Identity.GetAttribute("", "name"), new Version(manifest.Identity.GetAttribute("", "version")), manifest.Identity.GetAttribute("", "processorArchitecture"), manifest.Identity.GetAttribute("", "culture"));
		}

		// Token: 0x0400104F RID: 4175
		private ActivationContext m_context;

		// Token: 0x04001050 RID: 4176
		private object m_appId;

		// Token: 0x04001051 RID: 4177
		private object m_deployId;

		// Token: 0x04001052 RID: 4178
		private object m_defaultRequest;

		// Token: 0x04001053 RID: 4179
		private object m_appEvidence;
	}
}
