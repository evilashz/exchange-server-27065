using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200004D RID: 77
	internal abstract class ChangeTrackingFilter : IChangeTrackingFilter
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x0001CA64 File Offset: 0x0001AC64
		internal ChangeTrackingFilter(ChangeTrackingNode[] changeTrackingNodes, bool fillInMissingHashes)
		{
			this.changeTrackingNodes = new Dictionary<string, int>(changeTrackingNodes.Length);
			int num = 0;
			foreach (ChangeTrackingNode changeTrackingNode in changeTrackingNodes)
			{
				if (num == 0 && changeTrackingNode != ChangeTrackingNode.AllNodes)
				{
					throw new ArgumentException("The AllNodes node must be the first node in the list of changetrack nodes!");
				}
				this.changeTrackingNodes.Add(changeTrackingNode.QualifiedName, num++);
			}
			this.fillInMissingHashes = fillInMissingHashes;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
		public int?[] UpdateChangeTrackingInformation(XmlNode xmlItemRoot, int?[] oldChangeTrackingInformation)
		{
			this.seenNodes = new XmlNode[this.changeTrackingNodes.Values.Count];
			this.newChangeTrackingInformation = new int?[this.changeTrackingNodes.Values.Count];
			this.newChangeTrackingInformation[0] = this.ComputeHash(xmlItemRoot, true);
			if (this.fillInMissingHashes && oldChangeTrackingInformation != null)
			{
				AirSyncDiagnostics.Assert(this.newChangeTrackingInformation.Length >= oldChangeTrackingInformation.Length, "newChangeTrackingInformation.Length = {0}, oldChangeTrackingInformation.Length = {1}", new object[]
				{
					this.newChangeTrackingInformation.Length,
					oldChangeTrackingInformation.Length
				});
				int num = 0;
				while (num < this.changeTrackingNodes.Values.Count && num < oldChangeTrackingInformation.Length)
				{
					if (this.newChangeTrackingInformation[num] == null)
					{
						this.newChangeTrackingInformation[num] = oldChangeTrackingInformation[num];
					}
					num++;
				}
			}
			return this.newChangeTrackingInformation;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		public int?[] Filter(XmlNode xmlItemRoot, int?[] oldChangeTrackingInformation)
		{
			bool flag = false;
			this.UpdateChangeTrackingInformation(xmlItemRoot, oldChangeTrackingInformation);
			if (oldChangeTrackingInformation == null)
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter.Filter returning Xml intact with no previous info!");
				flag = true;
			}
			else
			{
				for (int i = 0; i < this.changeTrackingNodes.Count; i++)
				{
					if (i == 0)
					{
						if (this.newChangeTrackingInformation[i] != oldChangeTrackingInformation[i])
						{
							AirSyncDiagnostics.TraceInfo<int?, int?>(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter.Filter() detected AllNodes hash changed, old {0} new {1} !", oldChangeTrackingInformation[i], this.newChangeTrackingInformation[i]);
							flag = true;
							break;
						}
						xmlItemRoot.RemoveAll();
					}
					else if (i >= oldChangeTrackingInformation.Length || this.newChangeTrackingInformation[i] != oldChangeTrackingInformation[i])
					{
						AirSyncDiagnostics.TraceInfo<int, int?, int?>(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter.Filter() detected other node index {0} hash changed, old {1} new {2} !", i, (i >= oldChangeTrackingInformation.Length) ? new int?(-1) : oldChangeTrackingInformation[i], this.newChangeTrackingInformation[i]);
						xmlItemRoot.AppendChild(this.seenNodes[i]);
						flag = true;
					}
				}
			}
			if (!flag)
			{
				AirSyncDiagnostics.TraceInfo<int?[], int?[]>(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter detected a non-change to item. Old {0} New {1}!", oldChangeTrackingInformation, this.newChangeTrackingInformation);
				throw new ChangeTrackingItemRejectedException();
			}
			return this.newChangeTrackingInformation;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001CD70 File Offset: 0x0001AF70
		internal static bool IsEqual(int?[] changeTrackingInformationThis, int?[] changeTrackingInformationThat)
		{
			if (changeTrackingInformationThis == null && changeTrackingInformationThat == null)
			{
				throw new ArgumentNullException("changeTrackingInformationThis/That");
			}
			if (changeTrackingInformationThis == null || changeTrackingInformationThat == null)
			{
				return false;
			}
			if (changeTrackingInformationThis.Length != changeTrackingInformationThat.Length)
			{
				return false;
			}
			for (int i = 0; i < changeTrackingInformationThis.Length; i++)
			{
				if (changeTrackingInformationThis[i] != changeTrackingInformationThat[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001CDF4 File Offset: 0x0001AFF4
		private static bool IsContainer(XmlNode parent)
		{
			foreach (object obj in parent)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001CE54 File Offset: 0x0001B054
		private int? ComputeHash(XmlNode rootNode, bool shouldChangeTrack)
		{
			int? result = null;
			List<int> list = new List<int>(50);
			list.Add(ChangeTrackingNode.GetQualifiedName(rootNode).GetHashCode());
			foreach (object obj in rootNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string qualifiedName = ChangeTrackingNode.GetQualifiedName(xmlNode);
				if (shouldChangeTrack && this.changeTrackingNodes.ContainsKey(qualifiedName))
				{
					int num = this.changeTrackingNodes[qualifiedName];
					this.seenNodes[num] = xmlNode;
					if (ChangeTrackingFilter.IsContainer(xmlNode))
					{
						AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter.ComputeHash() Recursively computing hash for change tracked container {0}", qualifiedName);
						this.newChangeTrackingInformation[num] = this.ComputeHash(xmlNode, shouldChangeTrack);
						AirSyncDiagnostics.TraceInfo<string, int?>(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter.ComputeHash() Returned hash for change tracked container {0} = {1}", qualifiedName, this.newChangeTrackingInformation[num]);
					}
					else
					{
						int value = ChangeTrackingNode.GetQualifiedName(xmlNode).GetHashCode() ^ this.GetHashCode(xmlNode);
						this.newChangeTrackingInformation[num] = new int?(value);
						AirSyncDiagnostics.TraceInfo<string, int?>(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter.ComputeHash() Calculated change tracked node hash {0} {1}", qualifiedName, this.newChangeTrackingInformation[num]);
					}
				}
				else if (ChangeTrackingFilter.IsContainer(xmlNode))
				{
					int? arg = this.ComputeHash(xmlNode, false);
					if (arg != null)
					{
						list.Add(arg.Value);
						AirSyncDiagnostics.TraceInfo<string, string, int?>(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter.ComputeHash() Returned container node hash {0}{1} = {2}", xmlNode.NamespaceURI, xmlNode.Name, arg);
					}
				}
				else
				{
					int item = ChangeTrackingNode.GetQualifiedName(xmlNode).GetHashCode() ^ this.GetHashCode(xmlNode);
					list.Add(item);
					AirSyncDiagnostics.TraceInfo<string, string, int>(ExTraceGlobals.RequestsTracer, this, "ChangeTrackingFilter.ComputeHash() Calculated node hash {0}{1} = {2}", xmlNode.NamespaceURI, xmlNode.Name, item.GetHashCode());
				}
			}
			if (list.Count > 1)
			{
				list.Sort();
				StringBuilder stringBuilder = new StringBuilder(list.Count * 10);
				foreach (int num2 in list)
				{
					stringBuilder.Append(num2.ToString(CultureInfo.InvariantCulture));
				}
				result = new int?(stringBuilder.ToString().GetHashCode());
			}
			return result;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		private int GetHashCode(XmlNode xmlNode)
		{
			AirSyncBlobXmlNode airSyncBlobXmlNode = xmlNode as AirSyncBlobXmlNode;
			if (airSyncBlobXmlNode != null && airSyncBlobXmlNode.Stream != null)
			{
				return airSyncBlobXmlNode.GetHashCode();
			}
			return xmlNode.InnerText.GetHashCode();
		}

		// Token: 0x04000390 RID: 912
		private const int AllNodesIndex = 0;

		// Token: 0x04000391 RID: 913
		private IDictionary<string, int> changeTrackingNodes;

		// Token: 0x04000392 RID: 914
		private bool fillInMissingHashes;

		// Token: 0x04000393 RID: 915
		private int?[] newChangeTrackingInformation;

		// Token: 0x04000394 RID: 916
		private XmlNode[] seenNodes;
	}
}
