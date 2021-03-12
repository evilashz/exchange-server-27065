using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200017E RID: 382
	internal class OlcTopology
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00020C76 File Offset: 0x0001EE76
		public static OlcTopology Instance
		{
			get
			{
				return OlcTopology.instance.Value;
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00020C82 File Offset: 0x0001EE82
		private OlcTopology()
		{
			this.objLock = new object();
			this.LastRefreshed = DateTime.MinValue;
			this.rnd = new Random();
			this.xml = null;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00020CB2 File Offset: 0x0001EEB2
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x00020CBA File Offset: 0x0001EEBA
		private DateTime LastRefreshed { get; set; }

		// Token: 0x06000E6D RID: 3693 RVA: 0x00020CC4 File Offset: 0x0001EEC4
		private static XDocument Deserialize(string parsedXml)
		{
			Exception ex = null;
			XDocument result;
			try
			{
				result = XDocument.Parse(parsedXml);
			}
			catch (XmlException ex2)
			{
				ex = ex2;
				result = null;
			}
			catch (FormatException ex3)
			{
				ex = ex3;
				result = null;
			}
			catch (NotSupportedException ex4)
			{
				ex = ex4;
				result = null;
			}
			finally
			{
				if (ex != null)
				{
					CommonUtils.GenerateWatson(ex);
				}
			}
			return result;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00020D3C File Offset: 0x0001EF3C
		public string FindServerByLocalDatacenter()
		{
			string result;
			lock (this.objLock)
			{
				this.CheckAndInitialize();
				if (this.xml == null)
				{
					result = null;
				}
				else
				{
					List<XElement> list = new List<XElement>(from el in this.xml.Descendants("cluster")
					select el);
					if (list.Count == 0)
					{
						throw new ClusterNotFoundPermanentException();
					}
					XElement cluster = list[this.rnd.Next(list.Count)];
					result = this.FindServerByCluster(cluster);
				}
			}
			return result;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00020E28 File Offset: 0x0001F028
		public string FindServerByClusterIP(IPAddress clusterIp)
		{
			string result;
			lock (this.objLock)
			{
				this.CheckAndInitialize();
				if (this.xml == null)
				{
					throw new ClusterIPNotFoundPermanentException(clusterIp);
				}
				XElement xelement = (from el in this.xml.Descendants("cluster")
				where (string)el.Attribute("clusterIp") == clusterIp.ToString()
				select el).FirstOrDefault<XElement>();
				if (xelement == null)
				{
					throw new ClusterIPNotFoundPermanentException(clusterIp);
				}
				result = this.FindServerByCluster(xelement);
			}
			return result;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00020EDC File Offset: 0x0001F0DC
		private string FindServerByCluster(XElement cluster)
		{
			List<XElement> list = new List<XElement>(from el in cluster.Descendants("hostName")
			select el);
			if (list.Count == 0)
			{
				throw new ClusterNotFoundPermanentException();
			}
			XElement xelement = list[this.rnd.Next(list.Count)];
			return (string)xelement.Attribute("name");
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00020F5C File Offset: 0x0001F15C
		private void CheckAndInitialize()
		{
			if (this.xml == null || ConfigBase<OlcConfigSchema>.Provider.LastUpdated > this.LastRefreshed)
			{
				this.InitializeFromConfig();
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00020F84 File Offset: 0x0001F184
		private void InitializeFromConfig()
		{
			this.LastRefreshed = ConfigBase<OlcConfigSchema>.Provider.LastUpdated;
			string config = ConfigBase<OlcConfigSchema>.GetConfig<string>("OlcTopology");
			if (config == null)
			{
				this.xml = null;
				return;
			}
			XDocument xdocument = OlcTopology.Deserialize(config);
			if (xdocument != null)
			{
				Interlocked.Exchange<XDocument>(ref this.xml, xdocument);
			}
		}

		// Token: 0x0400081A RID: 2074
		private static Lazy<OlcTopology> instance = new Lazy<OlcTopology>(() => new OlcTopology(), LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x0400081B RID: 2075
		private object objLock;

		// Token: 0x0400081C RID: 2076
		private XDocument xml;

		// Token: 0x0400081D RID: 2077
		private Random rnd;
	}
}
