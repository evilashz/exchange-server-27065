using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueDigest;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x02000066 RID: 102
	[Cmdlet("Get", "QueueDigest", DefaultParameterSetName = "DagParameterSet")]
	[OutputType(new Type[]
	{
		typeof(QueueDigestPresentationObject)
	})]
	public sealed class GetQueueDigest : DataAccessTask<QueueDigestPresentationObject>
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000DA91 File Offset: 0x0000BC91
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "ServerParameterSet")]
		public MultiValuedProperty<ServerIdParameter> Server
		{
			get
			{
				return (MultiValuedProperty<ServerIdParameter>)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000DABB File Offset: 0x0000BCBB
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000DAD2 File Offset: 0x0000BCD2
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "DagParameterSet")]
		public MultiValuedProperty<DatabaseAvailabilityGroupIdParameter> Dag
		{
			get
			{
				return (MultiValuedProperty<DatabaseAvailabilityGroupIdParameter>)base.Fields["Dag"];
			}
			set
			{
				base.Fields["Dag"] = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000DAE5 File Offset: 0x0000BCE5
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "SiteParameterSet")]
		public MultiValuedProperty<AdSiteIdParameter> Site
		{
			get
			{
				return (MultiValuedProperty<AdSiteIdParameter>)base.Fields["Site"];
			}
			set
			{
				base.Fields["Site"] = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000DB0F File Offset: 0x0000BD0F
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000DB35 File Offset: 0x0000BD35
		[Parameter(Mandatory = true, ParameterSetName = "ForestParameterSet")]
		public SwitchParameter Forest
		{
			get
			{
				return (SwitchParameter)(base.Fields["Forest"] ?? false);
			}
			set
			{
				base.Fields["Forest"] = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000DB4D File Offset: 0x0000BD4D
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000DB6E File Offset: 0x0000BD6E
		[Parameter(Mandatory = false)]
		public QueueDigestGroupBy GroupBy
		{
			get
			{
				return (QueueDigestGroupBy)(base.Fields["GroupBy"] ?? QueueDigestGroupBy.NextHopDomain);
			}
			set
			{
				base.Fields["GroupBy"] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000DB86 File Offset: 0x0000BD86
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000DBA7 File Offset: 0x0000BDA7
		[Parameter(Mandatory = false)]
		public DetailsLevel DetailsLevel
		{
			get
			{
				return (DetailsLevel)(base.Fields["DetailsLevel"] ?? DetailsLevel.Normal);
			}
			set
			{
				base.Fields["DetailsLevel"] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000DBBF File Offset: 0x0000BDBF
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000DBD6 File Offset: 0x0000BDD6
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				this.queryFilter = new MonadFilter(value, this, ObjectSchema.GetInstance<ExtensibleQueueInfoSchema>()).InnerFilter;
				DateTimeConverter.ConvertQueryFilter(this.queryFilter);
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000DC14 File Offset: 0x0000BE14
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return this.resultSize;
			}
			set
			{
				this.resultSize = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000DC1D File Offset: 0x0000BE1D
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000DC43 File Offset: 0x0000BE43
		[Parameter(Mandatory = false)]
		public SwitchParameter Mtrt
		{
			get
			{
				return (SwitchParameter)(base.Fields["Mtrt"] ?? false);
			}
			set
			{
				base.Fields["Mtrt"] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000DC5B File Offset: 0x0000BE5B
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000DC81 File Offset: 0x0000BE81
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeE14Servers
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeE14Servers"] ?? false);
			}
			set
			{
				base.Fields["IncludeE14Servers"] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000DC99 File Offset: 0x0000BE99
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000DCA1 File Offset: 0x0000BEA1
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000DCAA File Offset: 0x0000BEAA
		internal QueryFilter QueryFilter
		{
			get
			{
				return this.queryFilter;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000DCB2 File Offset: 0x0000BEB2
		internal bool IsVerbose
		{
			get
			{
				return object.Equals(base.UserSpecifiedParameters["Verbose"], true);
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		protected override void InternalValidate()
		{
			try
			{
				if (base.ParameterSetName == "ForestParameterSet" && !this.Forest.ToBool())
				{
					base.WriteError(new LocalizedException(Strings.GetQueueDigestForestParameterCannotBeFalse), ErrorCategory.InvalidArgument, null);
				}
				this.ResolveParameters();
				base.InternalValidate();
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			if (this.IncludeE14Servers.ToBool() || !this.Mtrt.ToBool())
			{
				this.session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 330, "InternalStateReset", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\Queueviewer\\GetQueueDigest.cs");
				TransportConfigContainer transportConfigContainer = this.session.FindSingletonConfigurationObject<TransportConfigContainer>();
				Server server = this.session.FindLocalServer();
				this.impl = new GetQueueDigestWebServiceImpl(new GetQueueDigestCmdletAdapter(this), this.session, server.ServerSite, transportConfigContainer.DiagnosticsAggregationServicePort);
				return;
			}
			this.impl = new GetQueueDigestMtrtImpl(this);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		protected override void InternalProcessRecord()
		{
			try
			{
				base.WriteDebug("InternalProcessRecord called");
				if (this.ResultSize.IsUnlimited || this.ResultSize.Value != 0U)
				{
					base.InternalProcessRecord();
					this.impl.ProcessRecord();
				}
			}
			catch (LocalizedException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000DE4C File Offset: 0x0000C04C
		protected override IConfigDataProvider CreateSession()
		{
			return null;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000DE4F File Offset: 0x0000C04F
		private void ResolveParameters()
		{
			if (this.Server != null)
			{
				this.ResolveServer();
				return;
			}
			if (this.Dag != null)
			{
				this.ResolveDag();
				return;
			}
			if (this.Site != null)
			{
				this.ResolveAdSite();
				return;
			}
			this.ResolveForForest();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000DE84 File Offset: 0x0000C084
		private void ResolveForForest()
		{
			this.impl.ResolveForForest();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000DE94 File Offset: 0x0000C094
		private void ResolveDag()
		{
			foreach (DatabaseAvailabilityGroupIdParameter databaseAvailabilityGroupIdParameter in this.Dag)
			{
				DatabaseAvailabilityGroup dag = base.GetDataObject<DatabaseAvailabilityGroup>(databaseAvailabilityGroupIdParameter, this.session, null, new LocalizedString?(Strings.ErrorDagNotFound(databaseAvailabilityGroupIdParameter.ToString())), new LocalizedString?(Strings.ErrorDagNotUnique(databaseAvailabilityGroupIdParameter.ToString()))) as DatabaseAvailabilityGroup;
				this.impl.ResolveDag(dag);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000DF20 File Offset: 0x0000C120
		private void ResolveAdSite()
		{
			foreach (AdSiteIdParameter adSiteIdParameter in this.Site)
			{
				ADSite adSite = base.GetDataObject<ADSite>(adSiteIdParameter, this.session, null, new LocalizedString?(Strings.GetQueueDigestSiteNotFound(adSiteIdParameter)), new LocalizedString?(Strings.GetQueueDigestAmbiguosSite(adSiteIdParameter))) as ADSite;
				this.impl.ResolveAdSite(adSite);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		private void ResolveServer()
		{
			foreach (ServerIdParameter serverIdParameter in this.Server)
			{
				Server server = base.GetDataObject<Server>(serverIdParameter, this.session, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString()))) as Server;
				this.impl.ResolveServer(server);
			}
		}

		// Token: 0x04000146 RID: 326
		internal const string ServerParameterSetName = "ServerParameterSet";

		// Token: 0x04000147 RID: 327
		internal const string DagParameterSetName = "DagParameterSet";

		// Token: 0x04000148 RID: 328
		internal const string SiteParameterSetName = "SiteParameterSet";

		// Token: 0x04000149 RID: 329
		internal const string ForestParameterSetName = "ForestParameterSet";

		// Token: 0x0400014A RID: 330
		private const string ForestParameterNameKey = "Forest";

		// Token: 0x0400014B RID: 331
		private const string ServerParameterNameKey = "Server";

		// Token: 0x0400014C RID: 332
		private const string DagParameterNameKey = "Dag";

		// Token: 0x0400014D RID: 333
		private const string SiteParameterNameKey = "Site";

		// Token: 0x0400014E RID: 334
		private const string GroupByParameterNameKey = "GroupBy";

		// Token: 0x0400014F RID: 335
		private const string DetailsLevelParameterNameKey = "DetailsLevel";

		// Token: 0x04000150 RID: 336
		private const string FilterParameterNameKey = "Filter";

		// Token: 0x04000151 RID: 337
		private const string MtrtParemeterNameKey = "Mtrt";

		// Token: 0x04000152 RID: 338
		private const string IncludeE14ServersParameterNameKey = "IncludeE14Servers";

		// Token: 0x04000153 RID: 339
		private static readonly uint DefaultResultSize = 100U;

		// Token: 0x04000154 RID: 340
		private Unlimited<uint> resultSize = new Unlimited<uint>(GetQueueDigest.DefaultResultSize);

		// Token: 0x04000155 RID: 341
		private ITopologyConfigurationSession session;

		// Token: 0x04000156 RID: 342
		private QueryFilter queryFilter;

		// Token: 0x04000157 RID: 343
		private EnhancedTimeSpan timeout = EnhancedTimeSpan.FromSeconds(8.0);

		// Token: 0x04000158 RID: 344
		private GetQueueDigestImpl impl;
	}
}
