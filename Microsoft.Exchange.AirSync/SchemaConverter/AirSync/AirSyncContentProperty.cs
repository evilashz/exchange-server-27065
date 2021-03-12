using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000141 RID: 321
	internal class AirSyncContentProperty : AirSyncProperty, IContentProperty, IMIMEDataProperty, IMIMERelatedProperty, IProperty, IAirSyncAttachments
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x000591B9 File Offset: 0x000573B9
		public AirSyncContentProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
			base.ClientChangeTracked = true;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x000591D2 File Offset: 0x000573D2
		public IEnumerable<AirSyncAttachmentInfo> Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x000591DC File Offset: 0x000573DC
		public Stream Body
		{
			get
			{
				XmlNode xmlNode = base.XmlNode.SelectSingleNode("X:Data", base.NamespaceManager);
				if (xmlNode == null)
				{
					return null;
				}
				return new MemoryStream(Encoding.UTF8.GetBytes(xmlNode.InnerText));
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0005921A File Offset: 0x0005741A
		public bool IsOnSMIMEMessage
		{
			get
			{
				throw new NotImplementedException("IsOnSMIMEMessage should not be called");
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00059226 File Offset: 0x00057426
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x0005922E File Offset: 0x0005742E
		public Stream MIMEData { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00059237 File Offset: 0x00057437
		// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x0005923F File Offset: 0x0005743F
		public long MIMESize { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00059248 File Offset: 0x00057448
		public bool IsIrmErrorMessage
		{
			get
			{
				throw new NotImplementedException("IsIrmErrorMessage should not be called");
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00059254 File Offset: 0x00057454
		public void PreProcessProperty()
		{
			throw new NotImplementedException("PreProcessProperty should not be called");
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00059260 File Offset: 0x00057460
		public void PostProcessProperty()
		{
			throw new NotImplementedException("PostProcessProperty should not be called");
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0005926C File Offset: 0x0005746C
		public long Size
		{
			get
			{
				XmlNode xmlNode = base.XmlNode.SelectSingleNode("X:EstimatedDataSize", base.NamespaceManager);
				long result;
				if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.InnerText) && long.TryParse(xmlNode.InnerText, out result))
				{
					return result;
				}
				return 0L;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x000592B3 File Offset: 0x000574B3
		protected BodyType BodyType
		{
			get
			{
				return this.bodyType;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x000592BB File Offset: 0x000574BB
		protected Stream Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000592C3 File Offset: 0x000574C3
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x000592CB File Offset: 0x000574CB
		protected bool Truncated
		{
			get
			{
				return this.truncated;
			}
			set
			{
				this.truncated = value;
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000592D4 File Offset: 0x000574D4
		public Stream GetData(BodyType type, long truncationSize, out long totalDataSize, out IEnumerable<AirSyncAttachmentInfo> attachments)
		{
			throw new ConversionException("Conversion of BodyType needs to be handled from the XSO side");
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000592E0 File Offset: 0x000574E0
		public BodyType GetNativeType()
		{
			XmlNode xmlNode = base.XmlNode.SelectSingleNode("X:Type", base.NamespaceManager);
			if (xmlNode == null)
			{
				return BodyType.None;
			}
			string innerText;
			if ((innerText = xmlNode.InnerText) != null)
			{
				if (innerText == "1")
				{
					return BodyType.PlainText;
				}
				if (innerText == "2")
				{
					return BodyType.Html;
				}
				if (innerText == "3")
				{
					return BodyType.Rtf;
				}
				if (innerText == "4")
				{
					return BodyType.Mime;
				}
			}
			return BodyType.None;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0005935C File Offset: 0x0005755C
		public override void Unbind()
		{
			base.Unbind();
			this.attachments = null;
			this.bodyType = BodyType.None;
			this.data = null;
			this.MIMEData = null;
			this.MIMESize = 0L;
			this.Truncated = true;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00059390 File Offset: 0x00057590
		protected override void InternalCopyFrom(IProperty sourceProperty)
		{
			IContentProperty contentProperty = sourceProperty as IContentProperty;
			if (contentProperty == null)
			{
				throw new UnexpectedTypeException("IContentProperty", sourceProperty);
			}
			contentProperty.PreProcessProperty();
			try
			{
				long num = 0L;
				bool flag;
				long num3;
				if (BodyUtility.IsAskingForMIMEData(contentProperty, base.Options) && this.IsAcceptable(BodyType.Mime, out num, out flag))
				{
					int num2 = -1;
					this.bodyType = BodyType.Mime;
					this.MIMEData = contentProperty.MIMEData;
					if (base.Options.Contains("MIMETruncation"))
					{
						num2 = (int)base.Options["MIMETruncation"];
					}
					if (num2 >= 0 && (long)num2 < this.MIMEData.Length)
					{
						this.Truncated = true;
						this.MIMESize = (long)num2;
					}
					else
					{
						this.Truncated = false;
						this.MIMESize = this.MIMEData.Length;
					}
					num3 = contentProperty.MIMEData.Length;
				}
				else
				{
					this.bodyType = contentProperty.GetNativeType();
					if (this.bodyType == BodyType.None)
					{
						return;
					}
					num3 = contentProperty.Size;
					foreach (BodyType bodyType in AirSyncContentProperty.GetPrioritizedBodyTypes(this.bodyType))
					{
						try
						{
							long num4;
							if (this.ClientAccepts(contentProperty, bodyType, out num4, out num))
							{
								this.bodyType = bodyType;
								num3 = num4;
								break;
							}
						}
						catch (ObjectNotFoundException arg)
						{
							AirSyncDiagnostics.TraceInfo<BodyType, ObjectNotFoundException>(ExTraceGlobals.AirSyncTracer, this, "ClientAccepts({0}) has thrown {1}", bodyType, arg);
							num3 = 0L;
							this.bodyType = BodyType.None;
						}
					}
				}
				if (contentProperty.IsIrmErrorMessage)
				{
					this.Truncated = true;
				}
				base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
				base.XmlParentNode.AppendChild(base.XmlNode);
				XmlNode xmlNode = base.XmlNode;
				string elementName = "Type";
				int num5 = (int)this.bodyType;
				base.AppendChildNode(xmlNode, elementName, num5.ToString(CultureInfo.InvariantCulture));
				base.AppendChildNode(base.XmlNode, "EstimatedDataSize", num3.ToString(CultureInfo.InvariantCulture));
				if (this.Truncated)
				{
					base.AppendChildNode(base.XmlNode, "Truncated", "1");
				}
				this.CopyData();
			}
			finally
			{
				contentProperty.PostProcessProperty();
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00059604 File Offset: 0x00057804
		protected virtual bool ClientAccepts(IContentProperty srcProperty, BodyType type, out long estimatedDataSize, out long truncationSize)
		{
			estimatedDataSize = 0L;
			bool flag;
			if (this.IsAcceptable(type, out truncationSize, out flag))
			{
				if (truncationSize >= 0L && flag)
				{
					this.data = srcProperty.GetData(type, -1L, out estimatedDataSize, out this.attachments);
					if (this.data.Length >= truncationSize && estimatedDataSize > truncationSize)
					{
						this.data = null;
						this.Truncated = true;
						return false;
					}
					this.Truncated = false;
					estimatedDataSize = this.data.Length;
				}
				else
				{
					this.data = srcProperty.GetData(type, truncationSize, out estimatedDataSize, out this.attachments);
					if (truncationSize < 0L)
					{
						this.Truncated = false;
					}
					else if (this.data.Length >= truncationSize && estimatedDataSize > truncationSize)
					{
						this.Truncated = true;
					}
					else
					{
						this.Truncated = false;
					}
				}
				return true;
			}
			this.data = null;
			this.Truncated = true;
			return false;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000596E4 File Offset: 0x000578E4
		protected virtual void CopyData()
		{
			if (this.MIMEData != null && this.MIMESize > 0L)
			{
				base.AppendChildNode(base.XmlNode, "Data", this.MIMEData, this.MIMESize, XmlNodeType.Text);
				return;
			}
			if (this.data != null && this.data.Length > 0L)
			{
				base.AppendChildNode(base.XmlNode, "Data", this.data, -1L, XmlNodeType.Text);
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00059758 File Offset: 0x00057958
		protected bool IsAcceptable(BodyType type, out long truncationSize, out bool allOrNone)
		{
			allOrNone = false;
			truncationSize = -1L;
			List<BodyPreference> list = base.Options["BodyPreference"] as List<BodyPreference>;
			AirSyncDiagnostics.Assert(list != null);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Type == type)
				{
					allOrNone = list[i].AllOrNone;
					truncationSize = list[i].TruncationSize;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000597D0 File Offset: 0x000579D0
		private static List<BodyType> GetPrioritizedBodyTypes(BodyType nativeType)
		{
			List<BodyType> list = new List<BodyType>(5);
			list.Add(nativeType);
			BodyType item = nativeType;
			while (BodyUtility.GetLowerBodyType(ref item))
			{
				list.Add(item);
			}
			item = nativeType;
			while (BodyUtility.GetHigherBodyType(ref item))
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x04000A69 RID: 2665
		private BodyType bodyType;

		// Token: 0x04000A6A RID: 2666
		private Stream data;

		// Token: 0x04000A6B RID: 2667
		private bool truncated = true;

		// Token: 0x04000A6C RID: 2668
		private IEnumerable<AirSyncAttachmentInfo> attachments;
	}
}
