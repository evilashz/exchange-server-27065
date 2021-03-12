using System;
using System.IO;
using System.Security.Principal;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000057 RID: 87
	internal class WacFileRep
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x00009EB3 File Offset: 0x000080B3
		public WacFileRep(SecurityIdentifier logonSid, bool directFileAccessEnabled, bool externalServicesEnabled, bool wacOMEXEnabled, bool isEdit, bool isArchive) : this(logonSid, DateTime.UtcNow, directFileAccessEnabled, externalServicesEnabled, wacOMEXEnabled, isEdit, isArchive)
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009EC9 File Offset: 0x000080C9
		private WacFileRep(SecurityIdentifier logonSid, DateTime creationTime, bool directFileAccessEnabled, bool externalServicesEnabled, bool wacOMEXEnabled, bool isEdit, bool isArchive)
		{
			this.LogonSid = logonSid;
			this.CreationTime = creationTime;
			this.DirectFileAccessEnabled = directFileAccessEnabled;
			this.WacExternalServicesEnabled = externalServicesEnabled;
			this.OMEXEnabled = wacOMEXEnabled;
			this.IsEdit = isEdit;
			this.IsArchive = isArchive;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00009F06 File Offset: 0x00008106
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00009F0E File Offset: 0x0000810E
		public SecurityIdentifier LogonSid { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00009F17 File Offset: 0x00008117
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00009F1F File Offset: 0x0000811F
		public DateTime CreationTime { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00009F28 File Offset: 0x00008128
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00009F30 File Offset: 0x00008130
		public bool DirectFileAccessEnabled { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00009F39 File Offset: 0x00008139
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00009F41 File Offset: 0x00008141
		public bool OMEXEnabled { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00009F4A File Offset: 0x0000814A
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00009F52 File Offset: 0x00008152
		public bool WacExternalServicesEnabled { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00009F5B File Offset: 0x0000815B
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00009F63 File Offset: 0x00008163
		public bool IsEdit { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00009F6C File Offset: 0x0000816C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00009F74 File Offset: 0x00008174
		public bool IsArchive { get; private set; }

		// Token: 0x060002B1 RID: 689 RVA: 0x00009F80 File Offset: 0x00008180
		public static WacFileRep Parse(string fileRepAsString)
		{
			byte[] array = WacUtilities.FromBase64String(fileRepAsString);
			WacFileRep wacFileRep = null;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(array))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream, new UTF8Encoding()))
					{
						string text = binaryReader.ReadString();
						DateTime creationTime = new DateTime(binaryReader.ReadInt64());
						bool directFileAccessEnabled = binaryReader.ReadBoolean();
						bool externalServicesEnabled = binaryReader.ReadBoolean();
						bool wacOMEXEnabled = binaryReader.ReadBoolean();
						bool isEdit = binaryReader.ReadBoolean();
						bool isArchive = binaryReader.ReadBoolean();
						wacFileRep = new WacFileRep(new SecurityIdentifier(text), creationTime, directFileAccessEnabled, externalServicesEnabled, wacOMEXEnabled, isEdit, isArchive);
						if (!wacFileRep.LogonSid.IsAccountSid())
						{
							throw new OwaInvalidRequestException("WacFileRep contained an invalid SecurityIdentifier: " + text);
						}
					}
				}
			}
			catch (EndOfStreamException)
			{
				throw new OwaInvalidRequestException("Unable to parse WacRequest. (" + array.Length.ToString() + " bytes)");
			}
			return wacFileRep;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A084 File Offset: 0x00008284
		internal string Serialize()
		{
			byte[] inArray;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, new UTF8Encoding()))
				{
					binaryWriter.Write(this.LogonSid.Value);
					binaryWriter.Write(this.CreationTime.Ticks);
					binaryWriter.Write(this.DirectFileAccessEnabled);
					binaryWriter.Write(this.WacExternalServicesEnabled);
					binaryWriter.Write(this.OMEXEnabled);
					binaryWriter.Write(this.IsEdit);
					binaryWriter.Write(this.IsArchive);
				}
				inArray = memoryStream.ToArray();
			}
			return Convert.ToBase64String(inArray);
		}
	}
}
