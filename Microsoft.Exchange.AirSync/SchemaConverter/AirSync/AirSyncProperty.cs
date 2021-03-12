using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000122 RID: 290
	[Serializable]
	internal abstract class AirSyncProperty : PropertyBase
	{
		// Token: 0x06000F17 RID: 3863 RVA: 0x000561D4 File Offset: 0x000543D4
		public AirSyncProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport)
		{
			if (string.IsNullOrEmpty(airSyncTagName))
			{
				throw new ArgumentNullException("airSyncTagName");
			}
			this.airSyncTagNames = new string[1];
			bool[] array = new bool[1];
			this.tagBound = array;
			this.airSyncTagNames[0] = airSyncTagName;
			this.xmlNodeNamespace = xmlNodeNamespace;
			this.requiresClientSupport = requiresClientSupport;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0005622C File Offset: 0x0005442C
		public AirSyncProperty(string xmlNodeNamespace, string airSyncTagName1, string airSyncTagName2, bool requiresClientSupport)
		{
			if (string.IsNullOrEmpty(airSyncTagName1))
			{
				throw new ArgumentNullException("airSyncTagName1");
			}
			if (string.IsNullOrEmpty(airSyncTagName2))
			{
				throw new ArgumentNullException("airSyncTagName2");
			}
			this.airSyncTagNames = new string[2];
			bool[] array = new bool[2];
			this.tagBound = array;
			this.airSyncTagNames[0] = airSyncTagName1;
			this.airSyncTagNames[1] = airSyncTagName2;
			this.xmlNodeNamespace = xmlNodeNamespace;
			this.requiresClientSupport = requiresClientSupport;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000562A0 File Offset: 0x000544A0
		public AirSyncProperty(string xmlNodeNamespace, string[] airSyncTagNames, bool requiresClientSupport)
		{
			if (airSyncTagNames == null)
			{
				throw new ArgumentNullException("airSyncTagNames");
			}
			foreach (string value in airSyncTagNames)
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("A tag in airSyncTagNames");
				}
			}
			this.airSyncTagNames = airSyncTagNames;
			this.tagBound = new bool[airSyncTagNames.Length];
			this.xmlNodeNamespace = xmlNodeNamespace;
			this.requiresClientSupport = requiresClientSupport;
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0005630B File Offset: 0x0005450B
		public string[] AirSyncTagNames
		{
			get
			{
				return this.airSyncTagNames;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00056313 File Offset: 0x00054513
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x0005631B File Offset: 0x0005451B
		public bool ClientChangeTracked
		{
			get
			{
				return this.clientChangeTracked;
			}
			set
			{
				this.clientChangeTracked = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00056324 File Offset: 0x00054524
		public string Namespace
		{
			get
			{
				return this.xmlNodeNamespace;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0005632C File Offset: 0x0005452C
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00056334 File Offset: 0x00054534
		public IDictionary Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0005633D File Offset: 0x0005453D
		public bool RequiresClientSupport
		{
			get
			{
				return this.requiresClientSupport;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00056345 File Offset: 0x00054545
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x0005634D File Offset: 0x0005454D
		protected XmlNode XmlNode
		{
			get
			{
				return this.xmlNode;
			}
			set
			{
				this.xmlNode = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00056356 File Offset: 0x00054556
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0005635E File Offset: 0x0005455E
		protected XmlNode XmlParentNode
		{
			get
			{
				return this.xmlParentNode;
			}
			set
			{
				this.xmlParentNode = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00056368 File Offset: 0x00054568
		protected XmlNamespaceManager NamespaceManager
		{
			get
			{
				if (this.nsmgr == null && this.xmlNode != null)
				{
					this.nsmgr = new XmlNamespaceManager(this.xmlNode.OwnerDocument.NameTable);
					this.nsmgr.AddNamespace("X", this.xmlNodeNamespace);
				}
				return this.nsmgr;
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000563BC File Offset: 0x000545BC
		public void Bind(XmlNode xmlNode)
		{
			if (xmlNode == null)
			{
				throw new ArgumentNullException("xmlNode");
			}
			bool flag = false;
			int i = 0;
			while (i < this.airSyncTagNames.Length)
			{
				if (xmlNode.Name == this.airSyncTagNames[i])
				{
					if (!this.tagBound[i])
					{
						this.tagBound[i] = true;
						flag = true;
						break;
					}
					throw new ConversionException("There are multiple nodes with name " + xmlNode.Name);
				}
				else
				{
					i++;
				}
			}
			if (!flag)
			{
				throw new ConversionException("Cannot bind property to node " + xmlNode.InnerXml);
			}
			if (this.xmlNode == null)
			{
				this.xmlNode = xmlNode;
				this.xmlParentNode = xmlNode.ParentNode;
				base.State = PropertyState.Modified;
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00056469 File Offset: 0x00054669
		public void BindToParent(XmlNode xmlParentNode)
		{
			if (xmlParentNode == null)
			{
				throw new ArgumentNullException("xmlParentNode");
			}
			base.State = PropertyState.Uninitialized;
			this.xmlParentNode = xmlParentNode;
			this.xmlNode = null;
			AirSyncDiagnostics.Assert(PropertyState.Uninitialized == base.State);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0005649C File Offset: 0x0005469C
		public override void CopyFrom(IProperty srcProperty)
		{
			if (srcProperty == null)
			{
				throw new ArgumentNullException("srcProperty");
			}
			if (srcProperty.SchemaLinkId != base.SchemaLinkId)
			{
				throw new ConversionException("Schema link id's must match in CopyFrom()");
			}
			if (this.airSyncTagNames[0] == null)
			{
				throw new ConversionException("this.airSyncTagNames[0] is null");
			}
			if (!this.IsBound())
			{
				throw new ConversionException(string.Format(CultureInfo.InvariantCulture, "Property must be bound before we can copy to it; PropertyTag: {0}, NS: {1}", new object[]
				{
					this.airSyncTagNames[0],
					this.xmlNodeNamespace
				}));
			}
			PropertyState state = srcProperty.State;
			if (state != PropertyState.Uninitialized)
			{
				switch (state)
				{
				case PropertyState.Unmodified:
					goto IL_97;
				case PropertyState.NotSupported:
					return;
				}
				if (this.xmlNode != null)
				{
					this.xmlParentNode.RemoveChild(this.xmlNode);
					this.xmlNode = null;
				}
				if (srcProperty.State == PropertyState.SetToDefault)
				{
					this.InternalSetToDefault(srcProperty);
					return;
				}
				this.InternalCopyFrom(srcProperty);
				return;
			}
			IL_97:
			throw new ConversionException("Unexpected property state " + srcProperty.State + " in AirSyncProperty.CopyFrom()");
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00056599 File Offset: 0x00054799
		public bool IsBound()
		{
			return this.xmlParentNode != null;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x000565A8 File Offset: 0x000547A8
		public bool IsBoundToEmptyTag()
		{
			if (this.xmlNode == null)
			{
				AirSyncDiagnostics.TraceInfo<AirSyncProperty>(ExTraceGlobals.AirSyncTracer, this, "AirSyncProperty={0} IsBoundToEmptyTag=false due to null xmlNode", this);
				return false;
			}
			if (this.xmlNode.ChildNodes.Count == 0 && this.xmlNode.InnerText.Length == 0)
			{
				AirSyncDiagnostics.TraceInfo<AirSyncProperty>(ExTraceGlobals.AirSyncTracer, this, "AirSyncProperty={0} IsBoundToEmptyTag=true", this);
				return true;
			}
			AirSyncDiagnostics.TraceInfo<AirSyncProperty, int, int>(ExTraceGlobals.AirSyncTracer, this, "AirSyncProperty={0} IsBoundToEmptyTag=false, ChildNodes count={1}, InnerText length={2}", this, this.xmlNode.ChildNodes.Count, this.xmlNode.InnerText.Length);
			return false;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0005663C File Offset: 0x0005483C
		public void OutputEmptyNode()
		{
			if (!this.IsBound())
			{
				throw new ConversionException("Cannot output empty node without being bound to a parent");
			}
			XmlNode newChild = this.xmlParentNode.OwnerDocument.CreateElement(this.airSyncTagNames[0], this.xmlNodeNamespace);
			this.xmlParentNode.AppendChild(newChild);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00056688 File Offset: 0x00054888
		public override string ToString()
		{
			string text = this.CreateDebugString();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("(Base: {0}, ", base.ToString());
			stringBuilder.AppendFormat("xmlNodeNamespace: {0}, ", (this.xmlNodeNamespace != null) ? this.xmlNodeNamespace : "null");
			stringBuilder.AppendFormat("State: {0}, ", base.State);
			stringBuilder.AppendFormat("SchemaLinkId: {0}, ", base.SchemaLinkId);
			stringBuilder.AppendFormat("xmlParentNode: {0}, ", (this.xmlParentNode != null && this.xmlParentNode.Name != null) ? this.xmlParentNode.Name : "null");
			if (this.AirSyncTagNames != null)
			{
				for (int i = 0; i < this.airSyncTagNames.Length; i++)
				{
					stringBuilder.AppendFormat("airSyncTagNames[{0}]: {1}, ", i, (this.airSyncTagNames[i] != null) ? this.airSyncTagNames[i] : "null");
				}
			}
			stringBuilder.AppendFormat("clientChangeTracked: {0}, ", this.clientChangeTracked);
			stringBuilder.AppendFormat("AdditionalInfo ({0})) ", (text != null) ? text : "null");
			return stringBuilder.ToString();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000567B0 File Offset: 0x000549B0
		public override void Unbind()
		{
			try
			{
				this.xmlNode = null;
				this.xmlParentNode = null;
				this.nsmgr = null;
				base.State = PropertyState.Uninitialized;
				for (int i = 0; i < this.tagBound.Length; i++)
				{
					this.tagBound[i] = false;
				}
			}
			finally
			{
				base.Unbind();
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00056810 File Offset: 0x00054A10
		protected void AppendChildCData(XmlNode parentNode, string elementName, string value)
		{
			AirSyncDiagnostics.TraceInfo<string, string, string>(ExTraceGlobals.AirSyncTracer, this, "Creating CData Node={0} Value={1} to {2}", elementName, AirSyncProperty.GetValueToTrace(value, elementName), parentNode.Name);
			XmlNode xmlNode = this.xmlParentNode.OwnerDocument.CreateElement(elementName, this.xmlNodeNamespace);
			XmlCDataSection newChild = this.xmlParentNode.OwnerDocument.CreateCDataSection(value);
			xmlNode.AppendChild(newChild);
			parentNode.AppendChild(xmlNode);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00056878 File Offset: 0x00054A78
		protected void AppendChildNode(string elementName, string value)
		{
			AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.AirSyncTracer, this, "Creating Node={0} Value={1}", elementName, AirSyncProperty.GetValueToTrace(value, elementName));
			XmlNode xmlNode = this.xmlParentNode.OwnerDocument.CreateElement(elementName, this.xmlNodeNamespace);
			xmlNode.InnerText = value;
			this.xmlNode.AppendChild(xmlNode);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000568C9 File Offset: 0x00054AC9
		protected void AppendChildNode(XmlNode parentNode, string elementName, string value)
		{
			this.AppendChildNode(parentNode, elementName, value, this.xmlNodeNamespace);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000568DC File Offset: 0x00054ADC
		protected void AppendChildNode(XmlNode parentNode, string elementName, string value, string nodeNamespace)
		{
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.AirSyncTracer, this, "Appending Namespace = {0} Node={1} Value={2} to {3}", new object[]
			{
				nodeNamespace,
				elementName,
				AirSyncProperty.GetValueToTrace(value, elementName),
				parentNode.Name
			});
			XmlNode xmlNode = this.xmlParentNode.OwnerDocument.CreateElement(elementName, nodeNamespace);
			xmlNode.InnerText = value;
			parentNode.AppendChild(xmlNode);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00056940 File Offset: 0x00054B40
		protected XmlNode AppendChildNode(XmlNode parentNode, string elementName, Stream streamData, long dataSize, XmlNodeType orginalNodeType)
		{
			AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.AirSyncTracer, this, "Appending Node={0} to {1}", elementName, parentNode.Name);
			AirSyncBlobXmlNode airSyncBlobXmlNode = new AirSyncBlobXmlNode(null, elementName, this.xmlNodeNamespace, this.xmlParentNode.OwnerDocument);
			airSyncBlobXmlNode.Stream = streamData;
			airSyncBlobXmlNode.StreamDataSize = dataSize;
			airSyncBlobXmlNode.OriginalNodeType = orginalNodeType;
			parentNode.AppendChild(airSyncBlobXmlNode);
			return airSyncBlobXmlNode;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x000569A0 File Offset: 0x00054BA0
		protected XmlNode ReplaceValueOrCreateNode(XmlNode parentNode, string nodeName, string value)
		{
			XmlNode xmlNode = parentNode.SelectSingleNode("X:" + nodeName, this.NamespaceManager);
			if (xmlNode != null)
			{
				xmlNode.InnerText = value;
			}
			else
			{
				this.AppendChildNode(parentNode, nodeName, value);
			}
			return xmlNode;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000569DC File Offset: 0x00054BDC
		protected bool RemoveNode(XmlNode parentNode, string nodeName)
		{
			XmlNode xmlNode = parentNode.SelectSingleNode("X:" + nodeName, this.NamespaceManager);
			if (xmlNode != null)
			{
				parentNode.RemoveChild(xmlNode);
				return true;
			}
			return false;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00056A0F File Offset: 0x00054C0F
		protected void CreateAirSyncNode(string strData)
		{
			this.CreateAirSyncNode(strData, false);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00056A1C File Offset: 0x00054C1C
		protected void CreateAirSyncNode(string strData, bool allowEmptyNode)
		{
			AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.AirSyncTracer, this, "Creating Node={0} Value={1}", this.airSyncTagNames[0], AirSyncProperty.GetValueToTrace(strData, this.airSyncTagNames[0]));
			if (allowEmptyNode || !string.IsNullOrEmpty(strData))
			{
				this.xmlNode = this.xmlParentNode.OwnerDocument.CreateElement(this.airSyncTagNames[0], this.xmlNodeNamespace);
				this.xmlNode.InnerText = strData;
				this.xmlParentNode.AppendChild(this.xmlNode);
			}
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00056A9C File Offset: 0x00054C9C
		protected void CreateAirSyncNode(string airSyncTagName, string strData)
		{
			AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.AirSyncTracer, this, "Creating Node={0} Value={1}", airSyncTagName, AirSyncProperty.GetValueToTrace(strData, airSyncTagName));
			this.xmlNode = this.xmlParentNode.OwnerDocument.CreateElement(airSyncTagName, this.xmlNodeNamespace);
			this.xmlNode.InnerText = strData;
			this.xmlParentNode.AppendChild(this.xmlNode);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00056AFC File Offset: 0x00054CFC
		protected void CreateAirSyncNode(string airSyncTagName, Stream mimeData, long mimeSize, XmlNodeType originalNodeType)
		{
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.AirSyncTracer, this, "Creating Node={0}", airSyncTagName);
			AirSyncBlobXmlNode airSyncBlobXmlNode = new AirSyncBlobXmlNode(null, airSyncTagName, this.xmlNodeNamespace, this.xmlParentNode.OwnerDocument);
			airSyncBlobXmlNode.Stream = mimeData;
			airSyncBlobXmlNode.StreamDataSize = mimeSize;
			airSyncBlobXmlNode.OriginalNodeType = originalNodeType;
			this.xmlParentNode.AppendChild(airSyncBlobXmlNode);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00056B58 File Offset: 0x00054D58
		protected void CreateAirSyncNode(string airSyncTagName, byte[] data)
		{
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.AirSyncTracer, this, "Creating Node={0}", airSyncTagName);
			AirSyncBlobXmlNode airSyncBlobXmlNode = new AirSyncBlobXmlNode(null, airSyncTagName, this.xmlNodeNamespace, this.xmlParentNode.OwnerDocument);
			airSyncBlobXmlNode.ByteArray = data;
			this.xmlParentNode.AppendChild(airSyncBlobXmlNode);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00056BA3 File Offset: 0x00054DA3
		protected virtual string CreateDebugString()
		{
			return string.Empty;
		}

		// Token: 0x06000F3B RID: 3899
		protected abstract void InternalCopyFrom(IProperty srcProperty);

		// Token: 0x06000F3C RID: 3900 RVA: 0x00056BAA File Offset: 0x00054DAA
		protected virtual void InternalSetToDefault(IProperty srcProperty)
		{
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00056BAC File Offset: 0x00054DAC
		private static string GetValueToTrace(string value, string elementName)
		{
			if (!GlobalSettings.EnableMailboxLoggingVerboseMode)
			{
				foreach (string text in AirSyncUtility.PiiTags)
				{
					if (text.Equals(elementName, StringComparison.Ordinal))
					{
						return string.Format("{0} bytes", (value == null) ? 0 : value.Length);
					}
				}
			}
			return value;
		}

		// Token: 0x04000A4F RID: 2639
		private string[] airSyncTagNames;

		// Token: 0x04000A50 RID: 2640
		private bool clientChangeTracked;

		// Token: 0x04000A51 RID: 2641
		[NonSerialized]
		private IDictionary options;

		// Token: 0x04000A52 RID: 2642
		[NonSerialized]
		private XmlNode xmlNode;

		// Token: 0x04000A53 RID: 2643
		private string xmlNodeNamespace;

		// Token: 0x04000A54 RID: 2644
		private XmlNamespaceManager nsmgr;

		// Token: 0x04000A55 RID: 2645
		[NonSerialized]
		private XmlNode xmlParentNode;

		// Token: 0x04000A56 RID: 2646
		private bool requiresClientSupport;

		// Token: 0x04000A57 RID: 2647
		private bool[] tagBound;
	}
}
