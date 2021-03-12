using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000159 RID: 345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OABManifestAddressList
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00039DDB File Offset: 0x00037FDB
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x00039DE3 File Offset: 0x00037FE3
		public string Id { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00039DEC File Offset: 0x00037FEC
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x00039DF4 File Offset: 0x00037FF4
		public string DN { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00039DFD File Offset: 0x00037FFD
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x00039E05 File Offset: 0x00038005
		public string Name { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00039E0E File Offset: 0x0003800E
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x00039E16 File Offset: 0x00038016
		public OABManifestFile[] Files { get; set; }

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00039E20 File Offset: 0x00038020
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(200);
			stringBuilder.Append("Id=");
			stringBuilder.Append(this.Id);
			stringBuilder.Append(", DN:");
			stringBuilder.Append(this.DN);
			stringBuilder.Append(", Name:");
			stringBuilder.Append(this.Name);
			stringBuilder.Append(", file count:");
			stringBuilder.Append(this.Files.Length.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00039EAC File Offset: 0x000380AC
		internal static OABManifestAddressList Deserialize(Stream stream, XmlReader reader)
		{
			long position = stream.Position;
			if (!reader.IsStartElement("OAL"))
			{
				throw new InvalidDataException(string.Format("Invalid element at stream position {0} due non-expected element name: {1}", position, reader.Name));
			}
			string attribute;
			string attribute2;
			string attribute3;
			try
			{
				position = stream.Position;
				attribute = reader.GetAttribute("id");
				position = stream.Position;
				attribute2 = reader.GetAttribute("dn");
				position = stream.Position;
				attribute3 = reader.GetAttribute("name");
				position = stream.Position;
				reader.ReadStartElement();
			}
			catch (XmlException arg)
			{
				throw new InvalidDataException(string.Format("Invalid element at stream position {0} due exception: {1}", position, arg));
			}
			List<OABManifestFile> list = new List<OABManifestFile>(60);
			while (reader.NodeType == XmlNodeType.Element)
			{
				position = stream.Position;
				OABManifestFile oabmanifestFile = OABManifestFile.Deserialize(stream, reader);
				OABManifestAddressList.Tracer.TraceDebug<long, OABManifestFile>(0L, "Parsed file element from stream position {0}: {1}", position, oabmanifestFile);
				list.Add(oabmanifestFile);
			}
			position = stream.Position;
			try
			{
				reader.ReadEndElement();
			}
			catch (XmlException arg2)
			{
				throw new InvalidDataException(string.Format("Invalid element at stream position {0} due exception: {1}", position, arg2));
			}
			return new OABManifestAddressList
			{
				Id = attribute,
				DN = attribute2,
				Name = attribute3,
				Files = list.ToArray()
			};
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003A000 File Offset: 0x00038200
		internal void Serialize(XmlWriter writer)
		{
			writer.WriteStartElement("OAL");
			writer.WriteAttributeString("id", this.Id);
			writer.WriteAttributeString("dn", this.DN);
			writer.WriteAttributeString("name", this.Name);
			foreach (OABManifestFile oabmanifestFile in this.Files)
			{
				oabmanifestFile.Serialize(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0400076A RID: 1898
		private static readonly Trace Tracer = ExTraceGlobals.DataTracer;
	}
}
