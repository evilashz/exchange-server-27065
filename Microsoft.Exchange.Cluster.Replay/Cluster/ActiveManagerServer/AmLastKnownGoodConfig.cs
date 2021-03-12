using System;
using System.Linq;
using Microsoft.Exchange.Cluster.Common.Registry;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Rpc.ActiveManager;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000049 RID: 73
	internal class AmLastKnownGoodConfig
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00011F76 File Offset: 0x00010176
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00011F7E File Offset: 0x0001017E
		internal AmRole Role { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00011F87 File Offset: 0x00010187
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00011F8F File Offset: 0x0001018F
		internal AmServerName AuthoritativeServer { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00011F98 File Offset: 0x00010198
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00011FA0 File Offset: 0x000101A0
		internal AmServerName[] Members { get; private set; }

		// Token: 0x0600032A RID: 810 RVA: 0x00011FA9 File Offset: 0x000101A9
		private AmLastKnownGoodConfig()
		{
			this.Role = AmRole.Unknown;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00011FD4 File Offset: 0x000101D4
		internal static AmLastKnownGoodConfig ConstructLastKnownGoodConfigFromPersistentState()
		{
			AmLastKnownGoodConfig amLastKnownGoodConfig = null;
			string fromRegistry = AmLastKnownGoodConfig.GetFromRegistry();
			amLastKnownGoodConfig = new AmLastKnownGoodConfig();
			if (!string.IsNullOrEmpty(fromRegistry))
			{
				try
				{
					AmLastKnownConfigSerializable amLastKnownConfigSerializable = (AmLastKnownConfigSerializable)SerializationUtil.XmlToObject(fromRegistry, typeof(AmLastKnownConfigSerializable));
					amLastKnownGoodConfig.Role = (AmRole)amLastKnownConfigSerializable.Role;
					amLastKnownGoodConfig.AuthoritativeServer = new AmServerName(amLastKnownConfigSerializable.AuthoritativeServer);
					amLastKnownGoodConfig.Members = (from serverNameFqdn in amLastKnownConfigSerializable.Members
					select new AmServerName(serverNameFqdn)).ToArray<AmServerName>();
					amLastKnownGoodConfig.m_prevObjectXml = fromRegistry;
					string text = string.Empty;
					if (amLastKnownGoodConfig.Members != null)
					{
						string[] value = (from server in amLastKnownGoodConfig.Members
						select server.NetbiosName).ToArray<string>();
						text = string.Join(",", value);
					}
					ReplayCrimsonEvents.LastKnownGoodConfigInitialized.Log<AmRole, AmServerName, string>(amLastKnownGoodConfig.Role, amLastKnownGoodConfig.AuthoritativeServer, text);
				}
				catch (Exception ex)
				{
					ReplayCrimsonEvents.LastKnownGoodConfigSerializationError.Log<string, string>("Deserialize", ex.ToString());
				}
			}
			return amLastKnownGoodConfig;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000120F8 File Offset: 0x000102F8
		internal void Update(AmConfig cfg)
		{
			lock (this.m_locker)
			{
				this.Role = cfg.Role;
				if (cfg.IsPamOrSam)
				{
					this.Members = cfg.DagConfig.MemberServers;
					this.AuthoritativeServer = cfg.DagConfig.CurrentPAM;
				}
				else
				{
					this.Members = new AmServerName[]
					{
						AmServerName.LocalComputerName
					};
					this.AuthoritativeServer = AmServerName.LocalComputerName;
				}
				this.Persist();
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001219C File Offset: 0x0001039C
		private void Persist()
		{
			AmLastKnownConfigSerializable amLastKnownConfigSerializable = new AmLastKnownConfigSerializable();
			amLastKnownConfigSerializable.Role = (int)this.Role;
			amLastKnownConfigSerializable.AuthoritativeServer = this.AuthoritativeServer.Fqdn;
			amLastKnownConfigSerializable.Members = (from server in this.Members
			select server.Fqdn).ToArray<string>();
			string text = string.Empty;
			try
			{
				text = SerializationUtil.ObjectToXml(amLastKnownConfigSerializable);
				if (!SharedHelper.StringIEquals(text, this.m_prevObjectXml))
				{
					this.SaveToRegistry(text);
					this.m_prevObjectXml = text;
				}
			}
			catch (Exception ex)
			{
				ReplayCrimsonEvents.LastKnownGoodConfigSerializationError.Log<string, string>("Serialize", ex.ToString());
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00012254 File Offset: 0x00010454
		private void SaveToRegistry(string objectXml)
		{
			Exception ex;
			IRegistryKey registryKey = Dependencies.RegistryKeyProvider.TryOpenKey(SharedHelper.AmRegKeyRoot, ref ex);
			if (ex != null)
			{
				throw ex;
			}
			if (registryKey != null)
			{
				using (registryKey)
				{
					registryKey.SetValue("LastKnownGoodConfig", objectXml, RegistryValueKind.String);
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000122A8 File Offset: 0x000104A8
		private static string GetFromRegistry()
		{
			Exception ex;
			IRegistryKey registryKey = Dependencies.RegistryKeyProvider.TryOpenKey(SharedHelper.AmRegKeyRoot, ref ex);
			if (ex != null)
			{
				throw ex;
			}
			string result = string.Empty;
			if (registryKey != null)
			{
				using (registryKey)
				{
					result = (string)registryKey.GetValue("LastKnownGoodConfig", string.Empty);
				}
			}
			return result;
		}

		// Token: 0x04000137 RID: 311
		private object m_locker = new object();

		// Token: 0x04000138 RID: 312
		private string m_prevObjectXml;
	}
}
