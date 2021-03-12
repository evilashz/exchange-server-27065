using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000130 RID: 304
	internal class AirSyncBodyPartProperty : AirSyncProperty, IBodyPartProperty, IProperty, IAirSyncAttachments
	{
		// Token: 0x06000F62 RID: 3938 RVA: 0x000584B1 File Offset: 0x000566B1
		public AirSyncBodyPartProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, requiresClientSupport)
		{
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x000584BC File Offset: 0x000566BC
		public string Preview
		{
			get
			{
				throw new NotImplementedException("Preview should not be called");
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x000584C8 File Offset: 0x000566C8
		public IEnumerable<AirSyncAttachmentInfo> Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000584D0 File Offset: 0x000566D0
		public Stream GetData(BodyType type, long truncationSize, out long totalDataSize, out IEnumerable<AirSyncAttachmentInfo> attachments)
		{
			throw new NotImplementedException("GetData should not be called on the AirSyncBodyPartProperty");
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000584DC File Offset: 0x000566DC
		public override void Unbind()
		{
			base.Unbind();
			this.attachments = null;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000584EC File Offset: 0x000566EC
		protected override void InternalCopyFrom(IProperty sourceProperty)
		{
			IBodyPartProperty bodyPartProperty = sourceProperty as IBodyPartProperty;
			if (bodyPartProperty == null)
			{
				throw new UnexpectedTypeException("IBodyPartProperty", sourceProperty);
			}
			List<BodyPartPreference> list = base.Options["BodyPartPreference"] as List<BodyPartPreference>;
			if (list == null || list.Count <= 0)
			{
				return;
			}
			BodyPartPreference bodyPartPreference = list[0];
			long truncationSize = bodyPartPreference.TruncationSize;
			bool allOrNone = bodyPartPreference.AllOrNone;
			long num = 0L;
			bool flag = true;
			StatusCode statusCode = StatusCode.Success;
			Stream stream;
			if (truncationSize >= 0L && allOrNone)
			{
				stream = bodyPartProperty.GetData(bodyPartPreference.Type, -1L, out num, out this.attachments);
				if (stream == null)
				{
					statusCode = StatusCode.BodyPart_ConversationTooLarge;
				}
				else if (stream.Length >= truncationSize && num > truncationSize)
				{
					stream = null;
					flag = true;
				}
				else
				{
					flag = false;
					num = stream.Length;
				}
			}
			else
			{
				stream = bodyPartProperty.GetData(bodyPartPreference.Type, truncationSize, out num, out this.attachments);
				if (stream == null)
				{
					statusCode = StatusCode.BodyPart_ConversationTooLarge;
				}
				flag = (truncationSize >= 0L && (stream != null && stream.Length >= truncationSize && num > truncationSize));
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			base.XmlParentNode.AppendChild(base.XmlNode);
			XmlNode xmlNode = base.XmlNode;
			string elementName = "Status";
			int num2 = (int)statusCode;
			base.AppendChildNode(xmlNode, elementName, num2.ToString(CultureInfo.InvariantCulture));
			base.AppendChildNode(base.XmlNode, "Type", ((int)bodyPartPreference.Type).ToString(CultureInfo.InvariantCulture));
			base.AppendChildNode(base.XmlNode, "EstimatedDataSize", num.ToString(CultureInfo.InvariantCulture));
			if (flag)
			{
				base.AppendChildNode(base.XmlNode, "Truncated", "1");
			}
			if (stream != null && stream.Length > 0L)
			{
				base.AppendChildNode(base.XmlNode, "Data", stream, -1L, XmlNodeType.Text);
			}
			int preview = bodyPartPreference.Preview;
			if (preview == 0)
			{
				return;
			}
			string text = ((IBodyPartProperty)sourceProperty).Preview;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (text.Length > preview)
			{
				text = text.Remove(preview);
			}
			text = text.TrimEnd(new char[0]);
			if (!string.IsNullOrEmpty(text))
			{
				base.AppendChildNode(base.XmlNode, "Preview", text);
			}
		}

		// Token: 0x04000A5A RID: 2650
		private IEnumerable<AirSyncAttachmentInfo> attachments;
	}
}
