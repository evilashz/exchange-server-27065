using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000679 RID: 1657
	internal class VdirConfiguration
	{
		// Token: 0x060047BE RID: 18366 RVA: 0x000DA6F0 File Offset: 0x000D88F0
		private VdirConfiguration(Guid vDirObjectId)
		{
			this.vDirObjectId = vDirObjectId;
			this.expireTime = DateTime.MinValue;
		}

		// Token: 0x17002783 RID: 10115
		// (get) Token: 0x060047BF RID: 18367 RVA: 0x000DA738 File Offset: 0x000D8938
		internal static VdirConfiguration Instance
		{
			get
			{
				VdirConfiguration vdirConfiguration;
				try
				{
					VdirConfiguration.rwLock.AcquireReaderLock(-1);
					Guid vdirObjectGuid = VdirConfiguration.GetVdirObjectGuid();
					if (!VdirConfiguration.instances.ContainsKey(vdirObjectGuid))
					{
						LockCookie lockCookie = VdirConfiguration.rwLock.UpgradeToWriterLock(-1);
						try
						{
							if (!VdirConfiguration.instances.ContainsKey(vdirObjectGuid))
							{
								VdirConfiguration.instances.Add(vdirObjectGuid, new VdirConfiguration(vdirObjectGuid));
							}
						}
						finally
						{
							VdirConfiguration.rwLock.DowngradeFromWriterLock(ref lockCookie);
						}
					}
					vdirConfiguration = VdirConfiguration.instances[vdirObjectGuid];
				}
				finally
				{
					try
					{
						VdirConfiguration.rwLock.ReleaseReaderLock();
					}
					catch (ApplicationException)
					{
					}
				}
				if (vdirConfiguration.IsExpired)
				{
					lock (vdirConfiguration.snycObj)
					{
						vdirConfiguration.Renew();
					}
				}
				return vdirConfiguration;
			}
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x000DA81C File Offset: 0x000D8A1C
		private static Guid GetVdirObjectGuid()
		{
			Guid empty = Guid.Empty;
			if (HttpContext.Current != null)
			{
				string text = HttpContext.Current.Request.Headers["X-vDirObjectId"];
				if (!string.IsNullOrEmpty(text))
				{
					Guid.TryParse(text, out empty);
				}
			}
			return empty;
		}

		// Token: 0x17002784 RID: 10116
		// (get) Token: 0x060047C1 RID: 18369 RVA: 0x000DA862 File Offset: 0x000D8A62
		private bool IsExpired
		{
			get
			{
				return DateTime.UtcNow > this.expireTime;
			}
		}

		// Token: 0x17002785 RID: 10117
		// (get) Token: 0x060047C2 RID: 18370 RVA: 0x000DA874 File Offset: 0x000D8A74
		internal bool WindowsAuthenticationEnabled
		{
			get
			{
				return this.IsAuthenticationMethodEnabled(AuthenticationMethodFlags.WindowsIntegrated);
			}
		}

		// Token: 0x17002786 RID: 10118
		// (get) Token: 0x060047C3 RID: 18371 RVA: 0x000DA87E File Offset: 0x000D8A7E
		internal bool BasicAuthenticationEnabled
		{
			get
			{
				return this.IsAuthenticationMethodEnabled(AuthenticationMethodFlags.Basic);
			}
		}

		// Token: 0x17002787 RID: 10119
		// (get) Token: 0x060047C4 RID: 18372 RVA: 0x000DA887 File Offset: 0x000D8A87
		internal bool DigestAuthenticationEnabled
		{
			get
			{
				return this.IsAuthenticationMethodEnabled(AuthenticationMethodFlags.Digest);
			}
		}

		// Token: 0x17002788 RID: 10120
		// (get) Token: 0x060047C5 RID: 18373 RVA: 0x000DA890 File Offset: 0x000D8A90
		internal bool FormBasedAuthenticationEnabled
		{
			get
			{
				return this.IsAuthenticationMethodEnabled(AuthenticationMethodFlags.Fba);
			}
		}

		// Token: 0x17002789 RID: 10121
		// (get) Token: 0x060047C6 RID: 18374 RVA: 0x000DA899 File Offset: 0x000D8A99
		internal bool LiveIdAuthenticationEnabled
		{
			get
			{
				return this.IsAuthenticationMethodEnabled(AuthenticationMethodFlags.LiveIdFba);
			}
		}

		// Token: 0x1700278A RID: 10122
		// (get) Token: 0x060047C7 RID: 18375 RVA: 0x000DA8A3 File Offset: 0x000D8AA3
		internal bool AdfsAuthenticationEnabled
		{
			get
			{
				return this.IsAuthenticationMethodEnabled(AuthenticationMethodFlags.Adfs);
			}
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x000DA8B0 File Offset: 0x000D8AB0
		internal bool IsAuthenticationMethodEnabled(AuthenticationMethodFlags flag)
		{
			return (this.authenticationMethods & flag) > AuthenticationMethodFlags.None;
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x000DA8C0 File Offset: 0x000D8AC0
		private void Renew()
		{
			this.expireTime = DateTime.UtcNow.Add(VdirConfiguration.expirationPeriod);
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 222, "Renew", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\WebControls\\VdirConfiguration.cs");
			ADRawEntry virtualDirectoryObject = Utility.GetVirtualDirectoryObject(this.vDirObjectId, session, this.propertyDefinitions);
			if (virtualDirectoryObject != null)
			{
				this.authenticationMethods = (AuthenticationMethodFlags)virtualDirectoryObject[ADVirtualDirectorySchema.InternalAuthenticationMethodFlags];
			}
		}

		// Token: 0x04003030 RID: 12336
		private const string VDirObjectIdHeaderName = "X-vDirObjectId";

		// Token: 0x04003031 RID: 12337
		private readonly PropertyDefinition[] propertyDefinitions = new PropertyDefinition[]
		{
			ADVirtualDirectorySchema.InternalAuthenticationMethodFlags
		};

		// Token: 0x04003032 RID: 12338
		private static Dictionary<Guid, VdirConfiguration> instances = new Dictionary<Guid, VdirConfiguration>();

		// Token: 0x04003033 RID: 12339
		private static ReaderWriterLock rwLock = new ReaderWriterLock();

		// Token: 0x04003034 RID: 12340
		private static TimeSpan expirationPeriod = new TimeSpan(0, 3, 0, 0);

		// Token: 0x04003035 RID: 12341
		private readonly Guid vDirObjectId;

		// Token: 0x04003036 RID: 12342
		private DateTime expireTime;

		// Token: 0x04003037 RID: 12343
		private object snycObj = new object();

		// Token: 0x04003038 RID: 12344
		private AuthenticationMethodFlags authenticationMethods;
	}
}
