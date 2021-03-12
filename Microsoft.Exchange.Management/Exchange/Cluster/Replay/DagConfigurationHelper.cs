using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000889 RID: 2185
	[Serializable]
	public class DagConfigurationHelper
	{
		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06004BEA RID: 19434 RVA: 0x0013B4D1 File Offset: 0x001396D1
		// (set) Token: 0x06004BEB RID: 19435 RVA: 0x0013B4D9 File Offset: 0x001396D9
		public int Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				this.m_version = value;
			}
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06004BEC RID: 19436 RVA: 0x0013B4E2 File Offset: 0x001396E2
		// (set) Token: 0x06004BED RID: 19437 RVA: 0x0013B4EA File Offset: 0x001396EA
		public int ServersPerDag
		{
			get
			{
				return this.m_serversPerDag;
			}
			set
			{
				this.m_serversPerDag = value;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06004BEE RID: 19438 RVA: 0x0013B4F3 File Offset: 0x001396F3
		// (set) Token: 0x06004BEF RID: 19439 RVA: 0x0013B4FB File Offset: 0x001396FB
		public int DatabasesPerServer
		{
			get
			{
				return this.m_databasesPerServer;
			}
			set
			{
				this.m_databasesPerServer = value;
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06004BF0 RID: 19440 RVA: 0x0013B504 File Offset: 0x00139704
		// (set) Token: 0x06004BF1 RID: 19441 RVA: 0x0013B50C File Offset: 0x0013970C
		public int DatabasesPerVolume
		{
			get
			{
				return this.m_databasesPerVolume;
			}
			set
			{
				this.m_databasesPerVolume = value;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06004BF2 RID: 19442 RVA: 0x0013B515 File Offset: 0x00139715
		// (set) Token: 0x06004BF3 RID: 19443 RVA: 0x0013B51D File Offset: 0x0013971D
		public int CopiesPerDatabase
		{
			get
			{
				return this.m_copiesPerDatabase;
			}
			set
			{
				this.m_copiesPerDatabase = value;
			}
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x0013B526 File Offset: 0x00139726
		// (set) Token: 0x06004BF5 RID: 19445 RVA: 0x0013B52E File Offset: 0x0013972E
		public int MinCopiesPerDatabaseForMonitoring
		{
			get
			{
				return this.m_minCopiesPerDatabaseForMonitoring;
			}
			set
			{
				this.m_minCopiesPerDatabaseForMonitoring = value;
			}
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0013B537 File Offset: 0x00139737
		public DagConfigurationHelper()
		{
			this.m_version = 1;
			this.m_serversPerDag = 0;
			this.m_databasesPerServer = 0;
			this.m_databasesPerVolume = 0;
			this.m_copiesPerDatabase = 0;
			this.m_minCopiesPerDatabaseForMonitoring = 1;
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x0013B569 File Offset: 0x00139769
		public DagConfigurationHelper(int serversPerDag, int databasesPerServer, int databasesPerVolume, int copiesPerDatabase, int minCopiesPerDatabaseForMonitoring)
		{
			this.m_version = 1;
			this.m_serversPerDag = serversPerDag;
			this.m_databasesPerServer = databasesPerServer;
			this.m_databasesPerVolume = databasesPerVolume;
			this.m_copiesPerDatabase = copiesPerDatabase;
			this.m_minCopiesPerDatabaseForMonitoring = minCopiesPerDatabaseForMonitoring;
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x0013B5A0 File Offset: 0x001397A0
		public string Serialize()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(DagConfigurationHelper));
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				xmlSerializer.Serialize(stringWriter, this);
				this.m_serializedForm = stringWriter.ToString();
			}
			return this.m_serializedForm;
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x0013B600 File Offset: 0x00139800
		public static DagConfigurationHelper Deserialize(string configXML)
		{
			DagConfigurationHelper dagConfigurationHelper = null;
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(DagConfigurationHelper));
			DagConfigurationHelper result;
			using (StringReader stringReader = new StringReader(configXML))
			{
				try
				{
					object obj = xmlSerializer.Deserialize(stringReader);
					dagConfigurationHelper = (obj as DagConfigurationHelper);
					if (dagConfigurationHelper == null)
					{
						ExTraceGlobals.CmdletsTracer.TraceError<string, string>(0L, "Deserialized object {0} was not compatible with expected type {1}.", (obj != null) ? obj.GetType().Name : "(null)", typeof(DagConfigurationHelper).Name);
					}
				}
				catch (InvalidOperationException ex)
				{
					ExTraceGlobals.CmdletsTracer.TraceDebug<string, string>(0L, "Deserialization of object {0} failed:\n{1}", typeof(DagConfigurationHelper).Name, ex.ToString());
				}
				if (dagConfigurationHelper == null)
				{
					throw new FailedToDeserializeDagConfigurationXMLString(configXML, typeof(DagConfigurationHelper).Name);
				}
				result = dagConfigurationHelper;
			}
			return result;
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0013B6E0 File Offset: 0x001398E0
		internal static DatabaseAvailabilityGroupConfiguration ReadDagConfig(ADObjectId dagConfigId, IConfigDataProvider configSession)
		{
			DatabaseAvailabilityGroupConfiguration result = null;
			if (!ADObjectId.IsNullOrEmpty(dagConfigId))
			{
				result = (DatabaseAvailabilityGroupConfiguration)configSession.Read<DatabaseAvailabilityGroupConfiguration>(dagConfigId);
			}
			return result;
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x0013B708 File Offset: 0x00139908
		internal static DatabaseAvailabilityGroupConfiguration DagConfigIdParameterToDagConfig(DatabaseAvailabilityGroupConfigurationIdParameter dagConfigIdParam, IConfigDataProvider configSession)
		{
			IEnumerable<DatabaseAvailabilityGroupConfiguration> objects = dagConfigIdParam.GetObjects<DatabaseAvailabilityGroupConfiguration>(null, configSession);
			DatabaseAvailabilityGroupConfiguration result;
			using (IEnumerator<DatabaseAvailabilityGroupConfiguration> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new ManagementObjectNotFoundException(Strings.ErrorDagNotFound(dagConfigIdParam.ToString()));
				}
				DatabaseAvailabilityGroupConfiguration databaseAvailabilityGroupConfiguration = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new ManagementObjectAmbiguousException(Strings.ErrorDagNotUnique(dagConfigIdParam.ToString()));
				}
				result = databaseAvailabilityGroupConfiguration;
			}
			return result;
		}

		// Token: 0x04002D65 RID: 11621
		[NonSerialized]
		public const int ConfigVersion = 1;

		// Token: 0x04002D66 RID: 11622
		private int m_version;

		// Token: 0x04002D67 RID: 11623
		private int m_serversPerDag;

		// Token: 0x04002D68 RID: 11624
		private int m_databasesPerServer;

		// Token: 0x04002D69 RID: 11625
		private int m_databasesPerVolume;

		// Token: 0x04002D6A RID: 11626
		private int m_copiesPerDatabase;

		// Token: 0x04002D6B RID: 11627
		private int m_minCopiesPerDatabaseForMonitoring;

		// Token: 0x04002D6C RID: 11628
		private string m_serializedForm;
	}
}
