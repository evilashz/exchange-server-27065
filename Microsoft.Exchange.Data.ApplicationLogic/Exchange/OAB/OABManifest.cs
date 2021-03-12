using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x0200015A RID: 346
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OABManifest
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003A085 File Offset: 0x00038285
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0003A08D File Offset: 0x0003828D
		public OABManifestAddressList[] AddressLists { get; set; }

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003A098 File Offset: 0x00038298
		internal void Serialize(Stream stream)
		{
			XmlWriterSettings settings = new XmlWriterSettings
			{
				CheckCharacters = false,
				OmitXmlDeclaration = false,
				Indent = true,
				CloseOutput = false
			};
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
			{
				this.Serialize(xmlWriter);
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0003A0F4 File Offset: 0x000382F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(2000);
			for (int i = 0; i < this.AddressLists.Length; i++)
			{
				stringBuilder.Append("AddressList[");
				stringBuilder.Append(i.ToString());
				stringBuilder.Append("]=");
				stringBuilder.AppendLine(this.AddressLists[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003A160 File Offset: 0x00038360
		public OfflineAddressBookManifestVersion GetVersion()
		{
			if (this.AddressLists == null || this.AddressLists.Length == 0)
			{
				return null;
			}
			List<AddressListSequence> list = new List<AddressListSequence>(this.AddressLists.Length);
			foreach (OABManifestAddressList oabmanifestAddressList in this.AddressLists)
			{
				string id = oabmanifestAddressList.Id;
				uint? num = null;
				foreach (OABManifestFile oabmanifestFile in oabmanifestAddressList.Files)
				{
					if (oabmanifestFile.Type == OABDataFileType.Full)
					{
						num = new uint?(oabmanifestFile.Sequence);
						break;
					}
				}
				if (!string.IsNullOrEmpty(id) && num != null)
				{
					AddressListSequence item = new AddressListSequence
					{
						AddressListId = id,
						Sequence = num.Value
					};
					list.Add(item);
				}
			}
			return new OfflineAddressBookManifestVersion
			{
				AddressLists = list.ToArray()
			};
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003A250 File Offset: 0x00038450
		public static OABManifest LoadFromFile(string manifestFilePath)
		{
			OABManifest.Tracer.TraceFunction(0L, "OABManifest.LoadFromFile: start");
			OABManifest oabmanifest = null;
			TimeSpan timeout = TimeSpan.FromMilliseconds(100.0);
			try
			{
				bool flag = false;
				int num = 0;
				while (!flag && num < 3)
				{
					try
					{
						using (FileStream fileStream = new FileStream(manifestFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
						{
							oabmanifest = OABManifest.Deserialize(fileStream);
							OABManifest.Tracer.TraceDebug<string, OABManifest>(0L, "OABManifest.LoadFromFile: loaded OAB manifest from file {0}:\n\r{1}", manifestFilePath, oabmanifest);
							flag = true;
						}
					}
					catch (IOException arg)
					{
						OABManifest.Tracer.TraceError<string, IOException>(0L, "OABManifest.LoadFromFile: IOException opening file {0}: {1}", manifestFilePath, arg);
					}
					if (!flag)
					{
						Thread.Sleep(timeout);
					}
					num++;
				}
			}
			catch (InvalidDataException arg2)
			{
				OABManifest.Tracer.TraceError<string, InvalidDataException>(0L, "OABManifest.LoadFromFile: unable to load OAB manifest {0} due to exception: {1}", manifestFilePath, arg2);
			}
			OABManifest.Tracer.TraceFunction(0L, "OABManifest.LoadFromFile: end");
			return oabmanifest;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003A340 File Offset: 0x00038540
		public static OABManifest LoadFromMailbox(string fileSetId, MailboxSession session)
		{
			OABManifest.Tracer.TraceFunction(0L, "OABManifest.LoadFromMailbox: start");
			OABManifest result = null;
			MailboxFileStore mailboxFileStore = new MailboxFileStore("OAB");
			FileSetItem current = mailboxFileStore.GetCurrent(fileSetId, session);
			using (Stream singleFile = mailboxFileStore.GetSingleFile(current, "oab.xml", session))
			{
				if (singleFile != null)
				{
					try
					{
						result = OABManifest.Deserialize(singleFile);
						goto IL_6B;
					}
					catch (InvalidDataException arg)
					{
						OABManifest.Tracer.TraceError<string, InvalidDataException>(0L, "OABManifest.LoadFromMailbox: unable to load OAB manifest from mailbox fileset {0} due to exception: {1}", fileSetId, arg);
						goto IL_6B;
					}
				}
				OABManifest.Tracer.TraceError<string>(0L, "OABManifest.LoadFromMailbox: unable to load OAB manifest from mailbox fileset {0} because the manifest attachment cannot be found", fileSetId);
				IL_6B:;
			}
			OABManifest.Tracer.TraceFunction(0L, "OABManifest.LoadFromMailbox: end");
			return result;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003A3F4 File Offset: 0x000385F4
		public static OABManifest Deserialize(Stream stream)
		{
			XmlReaderSettings settings = new XmlReaderSettings
			{
				CheckCharacters = false,
				ConformanceLevel = ConformanceLevel.Document,
				IgnoreComments = true,
				IgnoreWhitespace = true,
				IgnoreProcessingInstructions = true,
				CloseInput = false
			};
			OABManifest result;
			using (XmlReader xmlReader = XmlReader.Create(stream, settings))
			{
				result = OABManifest.Deserialize(stream, xmlReader);
			}
			return result;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003A460 File Offset: 0x00038660
		private void Serialize(XmlWriter writer)
		{
			writer.WriteStartDocument();
			writer.WriteStartElement("OAB");
			foreach (OABManifestAddressList oabmanifestAddressList in this.AddressLists)
			{
				oabmanifestAddressList.Serialize(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003A4A4 File Offset: 0x000386A4
		private static OABManifest Deserialize(Stream stream, XmlReader reader)
		{
			long position = stream.Position;
			try
			{
				reader.MoveToContent();
				reader.ReadStartElement("OAB");
			}
			catch (XmlException arg)
			{
				throw new InvalidDataException(string.Format("Invalid element at position {0} due exception: {1}", position, arg));
			}
			List<OABManifestAddressList> list = new List<OABManifestAddressList>(1);
			while (reader.NodeType == XmlNodeType.Element)
			{
				OABManifestAddressList oabmanifestAddressList;
				try
				{
					oabmanifestAddressList = OABManifestAddressList.Deserialize(stream, reader);
				}
				catch (InvalidDataException arg2)
				{
					OABManifest.Tracer.TraceError<InvalidDataException>(0L, "Ignoring element due exception: {0}", arg2);
					continue;
				}
				OABManifest.Tracer.TraceDebug<OABManifestAddressList>(0L, "Parsed address list from stream: {0}", oabmanifestAddressList);
				list.Add(oabmanifestAddressList);
			}
			position = stream.Position;
			try
			{
				reader.ReadEndElement();
			}
			catch (XmlException arg3)
			{
				throw new InvalidDataException(string.Format("Invalid element at position {0} due exception: {1}", position, arg3));
			}
			OABManifest.Tracer.TraceDebug<int>(0L, "Parsed {0} address lists from stream", list.Count);
			return new OABManifest
			{
				AddressLists = list.ToArray()
			};
		}

		// Token: 0x0400076F RID: 1903
		private const string OABFolderName = "OAB";

		// Token: 0x04000770 RID: 1904
		private const string OABManifestFileName = "oab.xml";

		// Token: 0x04000771 RID: 1905
		private static readonly Trace Tracer = ExTraceGlobals.DataTracer;
	}
}
