using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000389 RID: 905
	[Serializable]
	public class ADServer : ADNonExchangeObject
	{
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000AE5E9 File Offset: 0x000AC7E9
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADServer.schema;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x000AE5F0 File Offset: 0x000AC7F0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADServer.mostDerivedClass;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000AE60B File Offset: 0x000AC80B
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x000AE61D File Offset: 0x000AC81D
		public string DnsHostName
		{
			get
			{
				return (string)this[ADServerSchema.DnsHostName];
			}
			internal set
			{
				this[ADServerSchema.DnsHostName] = value;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x000AE62B File Offset: 0x000AC82B
		public ADObjectId Site
		{
			get
			{
				if (base.Id != null)
				{
					return base.Id.Parent.Parent;
				}
				return null;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x000AE648 File Offset: 0x000AC848
		public bool IsInLocalSite
		{
			get
			{
				if (this.isInLocalSite == null)
				{
					string siteName = NativeHelpers.GetSiteName(false);
					this.isInLocalSite = new bool?(!string.IsNullOrEmpty(siteName) && this.Site != null && this.Site.Name.Equals(siteName, StringComparison.OrdinalIgnoreCase));
				}
				return this.isInLocalSite.Value;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000AE6A4 File Offset: 0x000AC8A4
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[ADServerSchema.Sid];
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x000AE6B6 File Offset: 0x000AC8B6
		public ADObjectId ServerReference
		{
			get
			{
				return (ADObjectId)this[ADServerSchema.ServerReference];
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000AE6C8 File Offset: 0x000AC8C8
		public ADObjectId DomainId
		{
			get
			{
				if (this.ServerReference != null)
				{
					return this.ServerReference.DomainId;
				}
				return null;
			}
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000AE6E0 File Offset: 0x000AC8E0
		internal bool IsAvailable()
		{
			ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Checking {0} for availability", this.DnsHostName);
			if (base.Session == null)
			{
				return false;
			}
			bool result;
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DnsHostName, true, ConsistencyMode.FullyConsistent, base.Session.NetworkCredential, ADSessionSettings.FromRootOrgScopeSet(), 147, "IsAvailable", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADServer.cs");
				topologyConfigurationSession.UseConfigNC = false;
				topologyConfigurationSession.UseGlobalCatalog = false;
				if (topologyConfigurationSession.FindComputerByHostName(this.DnsHostName) == null)
				{
					ExTraceGlobals.ADTopologyTracer.TraceError<string>((long)this.GetHashCode(), "FindComputerByHostName returned null for dns {0}", this.DnsHostName);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (SuitabilityException ex)
			{
				ExTraceGlobals.ADTopologyTracer.TraceError<string>((long)this.GetHashCode(), "Server unavailable because of a caught exception: {0}", ex.Message);
				result = false;
			}
			catch (ADTransientException ex2)
			{
				ExTraceGlobals.ADTopologyTracer.TraceError<string>((long)this.GetHashCode(), "Server unavailable because of a caught exception: {0}", ex2.Message);
				result = false;
			}
			catch (DataValidationException ex3)
			{
				ExTraceGlobals.ADTopologyTracer.TraceError<string>((long)this.GetHashCode(), "Server unavailable because of a caught exception: {0}", ex3.Message);
				result = false;
			}
			catch (ADExternalException ex4)
			{
				ExTraceGlobals.ADTopologyTracer.TraceError<string>((long)this.GetHashCode(), "Server unavailable because of a caught exception: {0}", ex4.Message);
				result = false;
			}
			catch (ADServerNotSuitableException ex5)
			{
				ExTraceGlobals.ADTopologyTracer.TraceError<string>((long)this.GetHashCode(), "Server unavailable because of a caught exception: {0}", ex5.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x0400195F RID: 6495
		private static ADServerSchema schema = ObjectSchema.GetInstance<ADServerSchema>();

		// Token: 0x04001960 RID: 6496
		private static string mostDerivedClass = "server";

		// Token: 0x04001961 RID: 6497
		private bool? isInLocalSite = null;
	}
}
