using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000AA3 RID: 2723
	public static class FolderSecurity
	{
		// Token: 0x06003A8C RID: 14988 RVA: 0x00095ADC File Offset: 0x00093CDC
		public static FolderSecurity.ExchangeFolderRights NormalizeFolderRights(FolderSecurity.ExchangeFolderRights folderRights)
		{
			FolderSecurity.ExchangeFolderRights exchangeFolderRights = folderRights & FolderSecurity.ExchangeFolderRights.FreeBusyAll;
			folderRights &= ~(FolderSecurity.ExchangeFolderRights.FreeBusySimple | FolderSecurity.ExchangeFolderRights.FreeBusyDetailed);
			FolderSecurity.ExchangeFolderRights exchangeFolderRights2 = FolderSecurity.FolderRightsFromSecurityDescriptorRights(FolderSecurity.SecurityDescriptorRightsFromFolderRights(folderRights, FolderSecurity.AceTarget.Folder), FolderSecurity.AceTarget.Folder);
			FolderSecurity.ExchangeFolderRights exchangeFolderRights3 = FolderSecurity.FolderRightsFromSecurityDescriptorRights(FolderSecurity.SecurityDescriptorRightsFromFolderRights(folderRights, FolderSecurity.AceTarget.Message), FolderSecurity.AceTarget.Message);
			return exchangeFolderRights2 | exchangeFolderRights3 | exchangeFolderRights;
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x00095B1C File Offset: 0x00093D1C
		public static FolderSecurity.ExchangeFolderRights FolderRightsFromSecurityDescriptorRights(FolderSecurity.ExchangeSecurityDescriptorFolderRights sdRights, FolderSecurity.AceTarget aceTarget)
		{
			FolderSecurity.ExchangeFolderRights exchangeFolderRights = FolderSecurity.ExchangeFolderRights.None;
			if (aceTarget == FolderSecurity.AceTarget.Folder)
			{
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.AppendMsg) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.AppendMsg)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.CreateSubfolder;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.Contact) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.Contact)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.Contact;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.FolderOwner) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.FolderOwner)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.Owner;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.Create;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.ViewItem) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.ViewItem)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.Visible;
				}
			}
			else if (aceTarget == FolderSecurity.AceTarget.Message)
			{
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.ReadAny;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteOwnProperty) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteOwnProperty)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.EditOwned;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.EditAny;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.DeleteOwnItem) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.DeleteOwnItem)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.DeleteOwned;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.Delete) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.Delete)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.DeleteAny;
				}
			}
			else if (aceTarget == FolderSecurity.AceTarget.FreeBusy)
			{
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadBody) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadBody)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.FreeBusySimple;
				}
				if ((sdRights & FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody) == FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody)
				{
					exchangeFolderRights |= FolderSecurity.ExchangeFolderRights.FreeBusyDetailed;
				}
			}
			return exchangeFolderRights;
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x00095C00 File Offset: 0x00093E00
		private static FolderSecurity.ExchangeSecurityDescriptorFolderRights SecurityDescriptorRightsFromFolderRights(FolderSecurity.ExchangeFolderRights folderRights, FolderSecurity.AceTarget aceTarget)
		{
			FolderSecurity.ExchangeSecurityDescriptorFolderRights exchangeSecurityDescriptorFolderRights = FolderSecurity.ExchangeSecurityDescriptorFolderRights.None;
			if (aceTarget == FolderSecurity.AceTarget.Folder)
			{
				if ((folderRights & FolderSecurity.ExchangeFolderRights.CreateSubfolder) == FolderSecurity.ExchangeFolderRights.CreateSubfolder)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.AppendMsg;
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.Owner) == FolderSecurity.ExchangeFolderRights.Owner)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.FolderOwner;
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.Contact) == FolderSecurity.ExchangeFolderRights.Contact)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.Contact;
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.Create) == FolderSecurity.ExchangeFolderRights.Create)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody;
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.Visible) == FolderSecurity.ExchangeFolderRights.Visible)
				{
					exchangeSecurityDescriptorFolderRights |= (FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadBody | FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty | FolderSecurity.ExchangeSecurityDescriptorFolderRights.Execute | FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadAttributes | FolderSecurity.ExchangeSecurityDescriptorFolderRights.ViewItem | FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadControl | FolderSecurity.ExchangeSecurityDescriptorFolderRights.Synchronize);
				}
				if ((folderRights & (FolderSecurity.ExchangeFolderRights.ReadAny | FolderSecurity.ExchangeFolderRights.Owner)) != FolderSecurity.ExchangeFolderRights.None)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.ViewItem;
				}
			}
			else if (aceTarget == FolderSecurity.AceTarget.Message)
			{
				if ((folderRights & FolderSecurity.ExchangeFolderRights.EditAny) == FolderSecurity.ExchangeFolderRights.EditAny)
				{
					exchangeSecurityDescriptorFolderRights |= (FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody | FolderSecurity.ExchangeSecurityDescriptorFolderRights.AppendMsg | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteAttributes | FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadControl | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteSD | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteOwner | FolderSecurity.ExchangeSecurityDescriptorFolderRights.Synchronize);
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.DeleteAny) == FolderSecurity.ExchangeFolderRights.DeleteAny)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.Delete;
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.EditOwned) == FolderSecurity.ExchangeFolderRights.EditOwned)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteOwnProperty;
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.DeleteOwned) == FolderSecurity.ExchangeFolderRights.DeleteOwned)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.DeleteOwnItem;
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.ReadAny) == FolderSecurity.ExchangeFolderRights.ReadAny)
				{
					exchangeSecurityDescriptorFolderRights |= (FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadBody | FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty | FolderSecurity.ExchangeSecurityDescriptorFolderRights.Execute | FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadAttributes | FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadControl | FolderSecurity.ExchangeSecurityDescriptorFolderRights.Synchronize);
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.ReadAny) == FolderSecurity.ExchangeFolderRights.ReadAny)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.ViewItem;
				}
			}
			else if (aceTarget == FolderSecurity.AceTarget.FreeBusy)
			{
				if ((folderRights & FolderSecurity.ExchangeFolderRights.FreeBusySimple) == FolderSecurity.ExchangeFolderRights.FreeBusySimple)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadBody;
				}
				if ((folderRights & FolderSecurity.ExchangeFolderRights.FreeBusyDetailed) == FolderSecurity.ExchangeFolderRights.FreeBusyDetailed)
				{
					exchangeSecurityDescriptorFolderRights |= FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteBody;
				}
			}
			return exchangeSecurityDescriptorFolderRights;
		}

		// Token: 0x040032F8 RID: 13048
		internal static NativeMethods.GENERIC_MAPPING GenericMapping = new NativeMethods.GENERIC_MAPPING
		{
			GenericRead = 1181833U,
			GenericWrite = 2048278U,
			GenericExecute = 1181856U,
			GenericAll = 2084863U
		};

		// Token: 0x02000AA4 RID: 2724
		[Flags]
		public enum ExchangeSecurityDescriptorFolderRights
		{
			// Token: 0x040032FA RID: 13050
			None = 0,
			// Token: 0x040032FB RID: 13051
			ReadBody = 1,
			// Token: 0x040032FC RID: 13052
			ListContents = 1,
			// Token: 0x040032FD RID: 13053
			WriteBody = 2,
			// Token: 0x040032FE RID: 13054
			CreateItem = 2,
			// Token: 0x040032FF RID: 13055
			AppendMsg = 4,
			// Token: 0x04003300 RID: 13056
			CreateContainer = 4,
			// Token: 0x04003301 RID: 13057
			ReadProperty = 8,
			// Token: 0x04003302 RID: 13058
			WriteProperty = 16,
			// Token: 0x04003303 RID: 13059
			Execute = 32,
			// Token: 0x04003304 RID: 13060
			Reserved1 = 64,
			// Token: 0x04003305 RID: 13061
			ReadAttributes = 128,
			// Token: 0x04003306 RID: 13062
			WriteAttributes = 256,
			// Token: 0x04003307 RID: 13063
			WriteOwnProperty = 512,
			// Token: 0x04003308 RID: 13064
			DeleteOwnItem = 1024,
			// Token: 0x04003309 RID: 13065
			ViewItem = 2048,
			// Token: 0x0400330A RID: 13066
			Owner = 16384,
			// Token: 0x0400330B RID: 13067
			Contact = 32768,
			// Token: 0x0400330C RID: 13068
			Delete = 65536,
			// Token: 0x0400330D RID: 13069
			ReadControl = 131072,
			// Token: 0x0400330E RID: 13070
			WriteSD = 262144,
			// Token: 0x0400330F RID: 13071
			WriteOwner = 524288,
			// Token: 0x04003310 RID: 13072
			Synchronize = 1048576,
			// Token: 0x04003311 RID: 13073
			GenericRead = 1181833,
			// Token: 0x04003312 RID: 13074
			GenericWrite = 2048278,
			// Token: 0x04003313 RID: 13075
			GenericExecute = 1181856,
			// Token: 0x04003314 RID: 13076
			GenericAll = 2084863,
			// Token: 0x04003315 RID: 13077
			FolderOwner = 868624,
			// Token: 0x04003316 RID: 13078
			AllFolder = 2083327,
			// Token: 0x04003317 RID: 13079
			AllMessage = 2035647,
			// Token: 0x04003318 RID: 13080
			IgnoreForCanonicalCheck = 32868,
			// Token: 0x04003319 RID: 13081
			WriteAnyAccess = 786710,
			// Token: 0x0400331A RID: 13082
			MessageGenericRead = 1181833,
			// Token: 0x0400331B RID: 13083
			MessageGenericWrite = 2031894,
			// Token: 0x0400331C RID: 13084
			MessageGenericExecute = 1181856,
			// Token: 0x0400331D RID: 13085
			MessageGenericAll = 2035647,
			// Token: 0x0400331E RID: 13086
			FolderGenericRead = 1181833,
			// Token: 0x0400331F RID: 13087
			FolderGenericWrite = 2048278,
			// Token: 0x04003320 RID: 13088
			FolderGenericExecute = 1181856,
			// Token: 0x04003321 RID: 13089
			FolderGenericAll = 2083327,
			// Token: 0x04003322 RID: 13090
			FreeBusySimple = 1,
			// Token: 0x04003323 RID: 13091
			FreeBusyDetailed = 2,
			// Token: 0x04003324 RID: 13092
			FreeBusyAll = 3
		}

		// Token: 0x02000AA5 RID: 2725
		[Flags]
		public enum ExchangeFolderRights
		{
			// Token: 0x04003326 RID: 13094
			None = 0,
			// Token: 0x04003327 RID: 13095
			ReadAny = 1,
			// Token: 0x04003328 RID: 13096
			Create = 2,
			// Token: 0x04003329 RID: 13097
			EditOwned = 8,
			// Token: 0x0400332A RID: 13098
			DeleteOwned = 16,
			// Token: 0x0400332B RID: 13099
			EditAny = 32,
			// Token: 0x0400332C RID: 13100
			DeleteAny = 64,
			// Token: 0x0400332D RID: 13101
			CreateSubfolder = 128,
			// Token: 0x0400332E RID: 13102
			Owner = 256,
			// Token: 0x0400332F RID: 13103
			Contact = 512,
			// Token: 0x04003330 RID: 13104
			Visible = 1024,
			// Token: 0x04003331 RID: 13105
			FreeBusySimple = 2048,
			// Token: 0x04003332 RID: 13106
			FreeBusyDetailed = 4096,
			// Token: 0x04003333 RID: 13107
			FreeBusyAll = 6144,
			// Token: 0x04003334 RID: 13108
			AllFolderRights = 7675,
			// Token: 0x04003335 RID: 13109
			Author = 1051,
			// Token: 0x04003336 RID: 13110
			ReadOnly = 1,
			// Token: 0x04003337 RID: 13111
			ReadWrite = 33
		}

		// Token: 0x02000AA6 RID: 2726
		public enum SecurityIdentifierType
		{
			// Token: 0x04003339 RID: 13113
			Unknown,
			// Token: 0x0400333A RID: 13114
			User,
			// Token: 0x0400333B RID: 13115
			Group
		}

		// Token: 0x02000AA7 RID: 2727
		public enum AceTarget
		{
			// Token: 0x0400333D RID: 13117
			Folder,
			// Token: 0x0400333E RID: 13118
			Message,
			// Token: 0x0400333F RID: 13119
			FreeBusy
		}

		// Token: 0x02000AA8 RID: 2728
		public class AclTableEntry
		{
			// Token: 0x06003A90 RID: 14992 RVA: 0x00095D5F File Offset: 0x00093F5F
			public AclTableEntry(long rowId, byte[] entryId, string name, FolderSecurity.ExchangeFolderRights rights)
			{
				this.name = name;
				this.rowId = rowId;
				this.entryId = entryId;
				this.rights = rights;
			}

			// Token: 0x17000E95 RID: 3733
			// (get) Token: 0x06003A91 RID: 14993 RVA: 0x00095D84 File Offset: 0x00093F84
			// (set) Token: 0x06003A92 RID: 14994 RVA: 0x00095D8C File Offset: 0x00093F8C
			public string Name
			{
				get
				{
					return this.name;
				}
				protected set
				{
					this.name = value;
				}
			}

			// Token: 0x17000E96 RID: 3734
			// (get) Token: 0x06003A93 RID: 14995 RVA: 0x00095D95 File Offset: 0x00093F95
			// (set) Token: 0x06003A94 RID: 14996 RVA: 0x00095D9D File Offset: 0x00093F9D
			public long RowId
			{
				get
				{
					return this.rowId;
				}
				protected set
				{
					this.rowId = value;
				}
			}

			// Token: 0x17000E97 RID: 3735
			// (get) Token: 0x06003A95 RID: 14997 RVA: 0x00095DA6 File Offset: 0x00093FA6
			public byte[] EntryId
			{
				get
				{
					return this.entryId;
				}
			}

			// Token: 0x17000E98 RID: 3736
			// (get) Token: 0x06003A96 RID: 14998 RVA: 0x00095DAE File Offset: 0x00093FAE
			// (set) Token: 0x06003A97 RID: 14999 RVA: 0x00095DB6 File Offset: 0x00093FB6
			public FolderSecurity.ExchangeFolderRights Rights
			{
				get
				{
					return this.rights;
				}
				set
				{
					this.rights = value;
				}
			}

			// Token: 0x17000E99 RID: 3737
			// (get) Token: 0x06003A98 RID: 15000 RVA: 0x00095DBF File Offset: 0x00093FBF
			public SecurityIdentifier SecurityIdentifier
			{
				get
				{
					return this.securityIdentifier;
				}
			}

			// Token: 0x17000E9A RID: 3738
			// (get) Token: 0x06003A99 RID: 15001 RVA: 0x00095DC7 File Offset: 0x00093FC7
			public bool IsGroup
			{
				get
				{
					return this.isGroup;
				}
			}

			// Token: 0x06003A9A RID: 15002 RVA: 0x00095DCF File Offset: 0x00093FCF
			public static List<FolderSecurity.AclTableEntry> ParseTableEntries(BinaryReader deserializer)
			{
				return FolderSecurity.AclTableEntry.ParseTableEntries<FolderSecurity.AclTableEntry>(deserializer, new Func<BinaryReader, FolderSecurity.AclTableEntry>(FolderSecurity.AclTableEntry.Parse));
			}

			// Token: 0x06003A9B RID: 15003 RVA: 0x00095DE4 File Offset: 0x00093FE4
			public static List<T> ParseTableEntries<T>(BinaryReader deserializer, Func<BinaryReader, T> parser) where T : FolderSecurity.AclTableEntry
			{
				int num = deserializer.ReadInt32();
				if (num != 1)
				{
					return null;
				}
				int num2 = deserializer.ReadInt32();
				if (num2 < 0 || (long)num2 > deserializer.BaseStream.Length - deserializer.BaseStream.Position || num2 > 20000)
				{
					throw new EndOfStreamException("Invalid array length");
				}
				List<T> list = new List<T>(num2);
				for (int i = 0; i < num2; i++)
				{
					T item = parser(deserializer);
					list.Add(item);
				}
				return list;
			}

			// Token: 0x06003A9C RID: 15004 RVA: 0x00095E5C File Offset: 0x0009405C
			public static void SerializeTableEntries<T>(List<T> tableEntries, BinaryWriter serializer) where T : FolderSecurity.AclTableEntry
			{
				serializer.Write(1);
				if (tableEntries == null)
				{
					serializer.Write(0);
					return;
				}
				serializer.Write(tableEntries.Count);
				foreach (T t in tableEntries)
				{
					FolderSecurity.AclTableEntry aclTableEntry = t;
					aclTableEntry.Serialize(serializer);
				}
			}

			// Token: 0x06003A9D RID: 15005 RVA: 0x00095ED0 File Offset: 0x000940D0
			public static FolderSecurity.AclTableEntry Parse(BinaryReader deserializer)
			{
				return new FolderSecurity.AclTableEntry(deserializer);
			}

			// Token: 0x06003A9E RID: 15006 RVA: 0x00095ED8 File Offset: 0x000940D8
			protected AclTableEntry(BinaryReader deserializer)
			{
				this.name = deserializer.ReadString();
				int num = deserializer.ReadInt32();
				if (num < 0)
				{
					throw new ArgumentException("EntryID length");
				}
				this.entryId = deserializer.ReadBytes(num);
				this.rights = (FolderSecurity.ExchangeFolderRights)deserializer.ReadInt32();
				int num2 = deserializer.ReadInt32();
				if (num2 < 0)
				{
					throw new ArgumentException("SID length");
				}
				this.securityIdentifier = new SecurityIdentifier(deserializer.ReadBytes(num2), 0);
				this.rowId = (long)deserializer.ReadUInt64();
				this.isGroup = (deserializer.ReadInt32() != 0);
			}

			// Token: 0x06003A9F RID: 15007 RVA: 0x00095F70 File Offset: 0x00094170
			public void Serialize(BinaryWriter serializer)
			{
				serializer.Write(this.name);
				serializer.Write(this.entryId.Length);
				serializer.Write(this.entryId);
				serializer.Write((int)this.rights);
				byte[] array = new byte[this.securityIdentifier.BinaryLength];
				this.securityIdentifier.GetBinaryForm(array, 0);
				serializer.Write(array.Length);
				serializer.Write(array);
				serializer.Write((ulong)this.rowId);
				serializer.Write(this.isGroup ? 1 : 0);
			}

			// Token: 0x06003AA0 RID: 15008 RVA: 0x00095FFB File Offset: 0x000941FB
			public void SetSecurityIdentifier(SecurityIdentifier securityIdentifier, bool isGroup)
			{
				this.securityIdentifier = securityIdentifier;
				this.isGroup = isGroup;
			}

			// Token: 0x04003340 RID: 13120
			private const int AclTableCurrentVersion = 1;

			// Token: 0x04003341 RID: 13121
			private readonly byte[] entryId;

			// Token: 0x04003342 RID: 13122
			private FolderSecurity.ExchangeFolderRights rights;

			// Token: 0x04003343 RID: 13123
			private SecurityIdentifier securityIdentifier;

			// Token: 0x04003344 RID: 13124
			private string name;

			// Token: 0x04003345 RID: 13125
			private long rowId;

			// Token: 0x04003346 RID: 13126
			private bool isGroup;
		}

		// Token: 0x02000AA9 RID: 2729
		public class AclTableAndSecurityDescriptorProperty
		{
			// Token: 0x06003AA1 RID: 15009 RVA: 0x0009600B File Offset: 0x0009420B
			public AclTableAndSecurityDescriptorProperty(ArraySegment<byte> serializedAclTable, Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> securityIdentifierToTypeMap, SecurityDescriptor securityDescriptor, SecurityDescriptor freeBusySecurityDescriptor)
			{
				this.serializedAclTable = serializedAclTable;
				this.securityIdentifierToTypeMap = securityIdentifierToTypeMap;
				this.securityDescriptor = securityDescriptor;
				this.freeBusySecurityDescriptor = freeBusySecurityDescriptor;
			}

			// Token: 0x06003AA2 RID: 15010 RVA: 0x00096030 File Offset: 0x00094230
			public static byte[] GetEmpty()
			{
				return FolderSecurity.AclTableAndSecurityDescriptorProperty.emptyPropertyBuffer;
			}

			// Token: 0x06003AA3 RID: 15011 RVA: 0x00096037 File Offset: 0x00094237
			public static bool IsEmpty(byte[] blob)
			{
				return ArrayComparer<byte>.Comparer.Equals(FolderSecurity.AclTableAndSecurityDescriptorProperty.GetEmpty(), blob);
			}

			// Token: 0x06003AA4 RID: 15012 RVA: 0x0009604C File Offset: 0x0009424C
			public static FolderSecurity.AclTableAndSecurityDescriptorProperty Parse(byte[] buffer)
			{
				ArraySegment<byte> arraySegment = new ArraySegment<byte>(Array<byte>.Empty);
				Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> dictionary = null;
				SecurityDescriptor securityDescriptor = null;
				SecurityDescriptor securityDescriptor2 = null;
				using (MemoryStream memoryStream = new MemoryStream(buffer, 0, buffer.Length, false, true))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						int num = binaryReader.ReadInt32();
						for (int i = 0; i < num; i++)
						{
							FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment segment = FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.Parse(binaryReader);
							switch (segment.SegmentType)
							{
							case FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType.Table:
								arraySegment = segment.Parse<ArraySegment<byte>>(binaryReader, new Func<BinaryReader, ArraySegment<byte>>(FolderSecurity.AclTableAndSecurityDescriptorProperty.ReadSegment));
								break;
							case FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType.SecurityIdentifierMap:
								dictionary = segment.Parse<Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType>>(binaryReader, new Func<BinaryReader, Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType>>(FolderSecurity.AclTableAndSecurityDescriptorProperty.ParseSecurityIdentifierToTypeMap));
								break;
							case FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType.SecurityDescriptor:
								securityDescriptor = segment.Parse<SecurityDescriptor>(binaryReader, new Func<BinaryReader, SecurityDescriptor>(FolderSecurity.AclTableAndSecurityDescriptorProperty.ParseSecurityDescriptor));
								break;
							case FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType.FreeBusySecurityDescriptor:
								securityDescriptor2 = segment.Parse<SecurityDescriptor>(binaryReader, new Func<BinaryReader, SecurityDescriptor>(FolderSecurity.AclTableAndSecurityDescriptorProperty.ParseSecurityDescriptor));
								break;
							default:
								segment.SkipData(binaryReader);
								break;
							}
						}
					}
				}
				return new FolderSecurity.AclTableAndSecurityDescriptorProperty(arraySegment, dictionary, securityDescriptor, securityDescriptor2);
			}

			// Token: 0x06003AA5 RID: 15013 RVA: 0x00096178 File Offset: 0x00094378
			internal static byte[] GetDefaultBlobForRootFolder()
			{
				return FolderSecurity.AclTableAndSecurityDescriptorProperty.defaultForRootFolder;
			}

			// Token: 0x06003AA6 RID: 15014 RVA: 0x00096180 File Offset: 0x00094380
			internal static byte[] GetDefaultBlobForGroupMailboxRootFolder(Guid mailboxGuid)
			{
				FolderSecurity.SecurityIdentifierAndFolderRights securityIdentifierAndFolderRights = new FolderSecurity.SecurityIdentifierAndFolderRights(SecurityIdentity.GetGroupSecurityIdentifier(mailboxGuid, SecurityIdentity.GroupMailboxMemberType.Owner), FolderSecurity.ExchangeFolderRights.ReadAny | FolderSecurity.ExchangeFolderRights.Create | FolderSecurity.ExchangeFolderRights.EditOwned | FolderSecurity.ExchangeFolderRights.DeleteOwned | FolderSecurity.ExchangeFolderRights.DeleteAny | FolderSecurity.ExchangeFolderRights.Visible, FolderSecurity.SecurityIdentifierType.User);
				FolderSecurity.SecurityIdentifierAndFolderRights securityIdentifierAndFolderRights2 = new FolderSecurity.SecurityIdentifierAndFolderRights(SecurityIdentity.GetGroupSecurityIdentifier(mailboxGuid, SecurityIdentity.GroupMailboxMemberType.Member), FolderSecurity.ExchangeFolderRights.Author, FolderSecurity.SecurityIdentifierType.User);
				return FolderSecurity.AclTableAndSecurityDescriptorProperty.GetBlobForSecurityDescriptor(new FolderSecurity.SecurityIdentifierAndFolderRights[]
				{
					securityIdentifierAndFolderRights,
					securityIdentifierAndFolderRights2,
					FolderSecurity.AclTableAndSecurityDescriptorProperty.anonymousNoRights,
					FolderSecurity.AclTableAndSecurityDescriptorProperty.everyoneNoRights
				});
			}

			// Token: 0x06003AA7 RID: 15015 RVA: 0x000961D8 File Offset: 0x000943D8
			internal static byte[] GetDefaultBlobForPublicFolders()
			{
				return FolderSecurity.AclTableAndSecurityDescriptorProperty.defaultForPublicFolders;
			}

			// Token: 0x06003AA8 RID: 15016 RVA: 0x000961DF File Offset: 0x000943DF
			internal static byte[] GetDefaultBlobForInternalSubmissionPublicFolder()
			{
				return FolderSecurity.AclTableAndSecurityDescriptorProperty.defaultForInternalSubmissionPublicFolder;
			}

			// Token: 0x06003AA9 RID: 15017 RVA: 0x000961E6 File Offset: 0x000943E6
			internal static byte[] CreateForChildFolder(byte[] parentdAclTableAndSD)
			{
				return FolderSecurity.AclTableAndSecurityDescriptorProperty.CreateForChildFolder(parentdAclTableAndSD, null, null, null, FolderSecurity.ExchangeFolderRights.None);
			}

			// Token: 0x06003AAA RID: 15018 RVA: 0x00096218 File Offset: 0x00094418
			internal static byte[] CreateForChildFolder(byte[] parentdAclTableAndSD, SecurityIdentifier delegateUser, byte[] delegateEntryId, string delegateDisplayName, FolderSecurity.ExchangeFolderRights delegateRights)
			{
				if (delegateUser == null)
				{
					return parentdAclTableAndSD;
				}
				FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = FolderSecurity.AclTableAndSecurityDescriptorProperty.Parse(parentdAclTableAndSD);
				RawSecurityDescriptor rawSecurityDescriptor = aclTableAndSecurityDescriptorProperty.SecurityDescriptor.ToRawSecurityDescriptorThrow();
				RawSecurityDescriptor rawSecurityDescriptor2 = (aclTableAndSecurityDescriptorProperty.FreeBusySecurityDescriptor != null) ? aclTableAndSecurityDescriptorProperty.FreeBusySecurityDescriptor.ToRawSecurityDescriptorThrow() : null;
				Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> dictionary = aclTableAndSecurityDescriptorProperty.SecurityIdentifierToTypeMap;
				if (rawSecurityDescriptor.DiscretionaryAcl != null)
				{
					FolderSecurity.AnnotatedAceList.RemoveAcesFromAcl(rawSecurityDescriptor.DiscretionaryAcl, delegateUser);
				}
				else
				{
					rawSecurityDescriptor.DiscretionaryAcl = new RawAcl(2, 4);
				}
				FolderSecurity.AnnotatedAceList.InsertUserAceToFolderAcl(rawSecurityDescriptor.DiscretionaryAcl, 0, new FolderSecurity.SecurityIdentifierAndFolderRights(delegateUser, delegateRights, FolderSecurity.SecurityIdentifierType.User), true);
				byte[] array = Array<byte>.Empty;
				if (aclTableAndSecurityDescriptorProperty.SerializedAclTable.Count != 0)
				{
					ArraySegment<byte> arraySegment = aclTableAndSecurityDescriptorProperty.SerializedAclTable;
					List<FolderSecurity.AclTableEntry> list;
					using (MemoryStream memoryStream = new MemoryStream(arraySegment.Array, arraySegment.Offset, arraySegment.Count))
					{
						using (BinaryReader binaryReader = new BinaryReader(memoryStream))
						{
							list = FolderSecurity.AclTableEntry.ParseTableEntries(binaryReader);
						}
					}
					if (list != null && list.Count != 0)
					{
						list.RemoveAll((FolderSecurity.AclTableEntry tableEntry) => tableEntry.SecurityIdentifier == delegateUser);
						long rowId;
						if (list.Count != 0)
						{
							rowId = (from entry in list
							select entry.RowId).Max() + 1L;
						}
						else
						{
							rowId = 1L;
						}
						FolderSecurity.AclTableEntry aclTableEntry = new FolderSecurity.AclTableEntry(rowId, delegateEntryId, delegateDisplayName, delegateRights);
						aclTableEntry.SetSecurityIdentifier(delegateUser, false);
						list.Add(aclTableEntry);
						using (MemoryStream memoryStream2 = new MemoryStream(200))
						{
							using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream2))
							{
								FolderSecurity.AclTableEntry.SerializeTableEntries<FolderSecurity.AclTableEntry>(list, binaryWriter);
								array = memoryStream2.ToArray();
							}
						}
					}
				}
				if (dictionary == null)
				{
					dictionary = new Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType>(1);
				}
				dictionary[delegateUser] = FolderSecurity.SecurityIdentifierType.User;
				FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty2 = new FolderSecurity.AclTableAndSecurityDescriptorProperty(new ArraySegment<byte>(array), dictionary, SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor), SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor2));
				return aclTableAndSecurityDescriptorProperty2.Serialize();
			}

			// Token: 0x06003AAB RID: 15019 RVA: 0x00096470 File Offset: 0x00094670
			internal static SecurityDescriptor CreateFolderSecurityDescriptor(RawAcl dacl)
			{
				RawSecurityDescriptor rawSecurityDescriptor = new RawSecurityDescriptor(ControlFlags.DiscretionaryAclPresent, FolderSecurity.AclTableAndSecurityDescriptorProperty.localSystemSID, FolderSecurity.AclTableAndSecurityDescriptorProperty.localSystemSID, null, dacl);
				return SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor);
			}

			// Token: 0x06003AAC RID: 15020 RVA: 0x00096498 File Offset: 0x00094698
			public byte[] Serialize()
			{
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream(512))
				{
					int num = 0;
					if (this.serializedAclTable.Count != 0)
					{
						num++;
					}
					if (this.securityIdentifierToTypeMap != null)
					{
						num++;
					}
					if (this.securityDescriptor != null)
					{
						num++;
					}
					if (this.freeBusySecurityDescriptor != null)
					{
						num++;
					}
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						binaryWriter.Write(num);
						if (this.serializedAclTable.Count != 0)
						{
							FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.Serialize<ArraySegment<byte>>(binaryWriter, FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType.Table, this.serializedAclTable, new FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.SerializerDelegate<ArraySegment<byte>>(FolderSecurity.AclTableAndSecurityDescriptorProperty.WriteSegment));
						}
						if (this.securityIdentifierToTypeMap != null)
						{
							FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.Serialize<Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType>>(binaryWriter, FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType.SecurityIdentifierMap, this.securityIdentifierToTypeMap, new FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.SerializerDelegate<Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType>>(FolderSecurity.AclTableAndSecurityDescriptorProperty.SerializeSecurityIdentifierToTypeMap));
						}
						if (this.securityDescriptor != null)
						{
							FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.Serialize<SecurityDescriptor>(binaryWriter, FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType.SecurityDescriptor, this.securityDescriptor, new FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.SerializerDelegate<SecurityDescriptor>(FolderSecurity.AclTableAndSecurityDescriptorProperty.SerializeSecurityDescriptor));
						}
						if (this.freeBusySecurityDescriptor != null)
						{
							FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.Serialize<SecurityDescriptor>(binaryWriter, FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType.FreeBusySecurityDescriptor, this.freeBusySecurityDescriptor, new FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.SerializerDelegate<SecurityDescriptor>(FolderSecurity.AclTableAndSecurityDescriptorProperty.SerializeSecurityDescriptor));
						}
					}
					result = memoryStream.ToArray();
				}
				return result;
			}

			// Token: 0x06003AAD RID: 15021 RVA: 0x000965C0 File Offset: 0x000947C0
			public SecurityDescriptor ComputeFreeBusySdFromFolderSd()
			{
				if (this.SecurityDescriptor == null)
				{
					return null;
				}
				FolderSecurity.AnnotatedAceList annotatedAceList = new FolderSecurity.AnnotatedAceList(this.SecurityDescriptor.ToRawSecurityDescriptorThrow(), null, (SecurityIdentifier sid) => FolderSecurity.SecurityIdentifierType.Unknown);
				IList<FolderSecurity.SecurityIdentifierAndFolderRights> securityIdentifierAndRightsList = annotatedAceList.GetSecurityIdentifierAndRightsList();
				bool flag = false;
				foreach (FolderSecurity.SecurityIdentifierAndFolderRights securityIdentifierAndFolderRights in securityIdentifierAndRightsList)
				{
					if (securityIdentifierAndFolderRights.SecurityIdentifier.IsWellKnown(WellKnownSidType.AnonymousSid))
					{
						securityIdentifierAndFolderRights.AllowRights = FolderSecurity.ExchangeFolderRights.None;
						securityIdentifierAndFolderRights.DenyRights = FolderSecurity.ExchangeFolderRights.FreeBusyAll;
					}
					else
					{
						if (securityIdentifierAndFolderRights.SecurityIdentifier.IsWellKnown(WellKnownSidType.WorldSid))
						{
							flag = true;
						}
						if ((securityIdentifierAndFolderRights.AllowRights & FolderSecurity.ExchangeFolderRights.ReadAny) == FolderSecurity.ExchangeFolderRights.ReadAny)
						{
							securityIdentifierAndFolderRights.AllowRights = FolderSecurity.ExchangeFolderRights.FreeBusyAll;
							securityIdentifierAndFolderRights.DenyRights = FolderSecurity.ExchangeFolderRights.None;
						}
						else
						{
							securityIdentifierAndFolderRights.AllowRights = FolderSecurity.ExchangeFolderRights.FreeBusySimple;
							securityIdentifierAndFolderRights.DenyRights = FolderSecurity.ExchangeFolderRights.FreeBusyDetailed;
						}
					}
				}
				if (!flag)
				{
					securityIdentifierAndRightsList.Add(new FolderSecurity.SecurityIdentifierAndFolderRights(FolderSecurity.AclTableAndSecurityDescriptorProperty.everyoneSID, FolderSecurity.ExchangeFolderRights.FreeBusySimple, FolderSecurity.ExchangeFolderRights.FreeBusyDetailed, FolderSecurity.SecurityIdentifierType.User));
				}
				return FolderSecurity.AclTableAndSecurityDescriptorProperty.CreateFolderSecurityDescriptor(FolderSecurity.AnnotatedAceList.BuildFreeBusyCanonicalAceList(securityIdentifierAndRightsList));
			}

			// Token: 0x17000E9B RID: 3739
			// (get) Token: 0x06003AAE RID: 15022 RVA: 0x000966DC File Offset: 0x000948DC
			public ArraySegment<byte> SerializedAclTable
			{
				get
				{
					return this.serializedAclTable;
				}
			}

			// Token: 0x17000E9C RID: 3740
			// (get) Token: 0x06003AAF RID: 15023 RVA: 0x000966E4 File Offset: 0x000948E4
			public Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> SecurityIdentifierToTypeMap
			{
				get
				{
					return this.securityIdentifierToTypeMap;
				}
			}

			// Token: 0x17000E9D RID: 3741
			// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000966EC File Offset: 0x000948EC
			public SecurityDescriptor SecurityDescriptor
			{
				get
				{
					return this.securityDescriptor;
				}
			}

			// Token: 0x17000E9E RID: 3742
			// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000966F4 File Offset: 0x000948F4
			public SecurityDescriptor FreeBusySecurityDescriptor
			{
				get
				{
					return this.freeBusySecurityDescriptor;
				}
			}

			// Token: 0x06003AB2 RID: 15026 RVA: 0x000966FC File Offset: 0x000948FC
			private static byte[] GetBlobForSecurityDescriptor(params FolderSecurity.SecurityIdentifierAndFolderRights[] rights)
			{
				RawAcl dacl = FolderSecurity.AnnotatedAceList.BuildFolderCanonicalAceList(rights);
				SecurityDescriptor securityDescriptor = FolderSecurity.AclTableAndSecurityDescriptorProperty.CreateFolderSecurityDescriptor(dacl);
				FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = new FolderSecurity.AclTableAndSecurityDescriptorProperty(new ArraySegment<byte>(Array<byte>.Empty), null, securityDescriptor, null);
				return aclTableAndSecurityDescriptorProperty.Serialize();
			}

			// Token: 0x06003AB3 RID: 15027 RVA: 0x00096730 File Offset: 0x00094930
			private static ArraySegment<byte> AllocateSegment(BinaryWriter writer, int segmentSize)
			{
				MemoryStream memoryStream = writer.BaseStream as MemoryStream;
				writer.Write(segmentSize);
				memoryStream.SetLength(memoryStream.Position + (long)segmentSize);
				ArraySegment<byte> result = new ArraySegment<byte>(memoryStream.GetBuffer(), (int)memoryStream.Position, segmentSize);
				memoryStream.Seek((long)segmentSize, SeekOrigin.Current);
				return result;
			}

			// Token: 0x06003AB4 RID: 15028 RVA: 0x0009677F File Offset: 0x0009497F
			private static void WriteSegment(BinaryWriter writer, ArraySegment<byte> segment)
			{
				writer.Write(segment.Count);
				writer.Write(segment.Array, segment.Offset, segment.Count);
			}

			// Token: 0x06003AB5 RID: 15029 RVA: 0x000967AC File Offset: 0x000949AC
			private static ArraySegment<byte> ReadSegment(BinaryReader reader)
			{
				MemoryStream memoryStream = reader.BaseStream as MemoryStream;
				int num = reader.ReadInt32();
				if (num < 0)
				{
					throw new ArgumentException("Segment length cannot be negative");
				}
				ArraySegment<byte> result = new ArraySegment<byte>(memoryStream.GetBuffer(), (int)memoryStream.Position, num);
				memoryStream.Seek((long)num, SeekOrigin.Current);
				return result;
			}

			// Token: 0x06003AB6 RID: 15030 RVA: 0x000967FC File Offset: 0x000949FC
			private static void SerializeSecurityDescriptor(BinaryWriter writer, SecurityDescriptor securityDescriptor)
			{
				ArraySegment<byte> arraySegment = FolderSecurity.AclTableAndSecurityDescriptorProperty.AllocateSegment(writer, securityDescriptor.BinaryForm.Length);
				Array.Copy(securityDescriptor.BinaryForm, 0, arraySegment.Array, arraySegment.Offset, securityDescriptor.BinaryForm.Length);
			}

			// Token: 0x06003AB7 RID: 15031 RVA: 0x0009683C File Offset: 0x00094A3C
			private static SecurityDescriptor ParseSecurityDescriptor(BinaryReader reader)
			{
				ArraySegment<byte> arraySegment = FolderSecurity.AclTableAndSecurityDescriptorProperty.ReadSegment(reader);
				byte[] array = new byte[arraySegment.Count];
				Array.Copy(arraySegment.Array, arraySegment.Offset, array, 0, arraySegment.Count);
				return new SecurityDescriptor(array);
			}

			// Token: 0x06003AB8 RID: 15032 RVA: 0x00096880 File Offset: 0x00094A80
			private static void SerializeSecurityIdentifier(BinaryWriter writer, SecurityIdentifier securityIdentifier)
			{
				ArraySegment<byte> arraySegment = FolderSecurity.AclTableAndSecurityDescriptorProperty.AllocateSegment(writer, securityIdentifier.BinaryLength);
				securityIdentifier.GetBinaryForm(arraySegment.Array, arraySegment.Offset);
			}

			// Token: 0x06003AB9 RID: 15033 RVA: 0x000968B0 File Offset: 0x00094AB0
			private static SecurityIdentifier ParseSecurityIdentifier(BinaryReader reader)
			{
				ArraySegment<byte> arraySegment = FolderSecurity.AclTableAndSecurityDescriptorProperty.ReadSegment(reader);
				return new SecurityIdentifier(arraySegment.Array, arraySegment.Offset);
			}

			// Token: 0x06003ABA RID: 15034 RVA: 0x000968D8 File Offset: 0x00094AD8
			private static void SerializeSecurityIdentifierToTypeMap(BinaryWriter writer, Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> securityIdentifierToTypeMap)
			{
				writer.Write(securityIdentifierToTypeMap.Count);
				foreach (SecurityIdentifier securityIdentifier in securityIdentifierToTypeMap.Keys)
				{
					FolderSecurity.AclTableAndSecurityDescriptorProperty.SerializeSecurityIdentifier(writer, securityIdentifier);
					writer.Write((int)securityIdentifierToTypeMap[securityIdentifier]);
				}
			}

			// Token: 0x06003ABB RID: 15035 RVA: 0x00096944 File Offset: 0x00094B44
			private static Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> ParseSecurityIdentifierToTypeMap(BinaryReader reader)
			{
				int num = reader.ReadInt32();
				if (num < 0)
				{
					throw new ArgumentException("Map size cannot be negative");
				}
				Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> dictionary = new Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType>(Math.Min(num, 50));
				for (int i = 0; i < num; i++)
				{
					SecurityIdentifier key = FolderSecurity.AclTableAndSecurityDescriptorProperty.ParseSecurityIdentifier(reader);
					FolderSecurity.SecurityIdentifierType value = (FolderSecurity.SecurityIdentifierType)reader.ReadInt32();
					dictionary[key] = value;
				}
				return dictionary;
			}

			// Token: 0x04003347 RID: 13127
			private static readonly byte[] emptyPropertyBuffer = new FolderSecurity.AclTableAndSecurityDescriptorProperty(new ArraySegment<byte>(Array<byte>.Empty), null, null, null).Serialize();

			// Token: 0x04003348 RID: 13128
			private static SecurityIdentifier localSystemSID = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);

			// Token: 0x04003349 RID: 13129
			private static SecurityIdentifier everyoneSID = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

			// Token: 0x0400334A RID: 13130
			private static SecurityIdentifier anonymousSID = new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);

			// Token: 0x0400334B RID: 13131
			private static readonly FolderSecurity.SecurityIdentifierAndFolderRights anonymousNoRights = new FolderSecurity.SecurityIdentifierAndFolderRights(FolderSecurity.AclTableAndSecurityDescriptorProperty.anonymousSID, FolderSecurity.ExchangeFolderRights.None, FolderSecurity.SecurityIdentifierType.User);

			// Token: 0x0400334C RID: 13132
			private static readonly FolderSecurity.SecurityIdentifierAndFolderRights everyoneNoRights = new FolderSecurity.SecurityIdentifierAndFolderRights(FolderSecurity.AclTableAndSecurityDescriptorProperty.everyoneSID, FolderSecurity.ExchangeFolderRights.None, FolderSecurity.SecurityIdentifierType.User);

			// Token: 0x0400334D RID: 13133
			private static byte[] defaultForRootFolder = FolderSecurity.AclTableAndSecurityDescriptorProperty.GetBlobForSecurityDescriptor(new FolderSecurity.SecurityIdentifierAndFolderRights[]
			{
				FolderSecurity.AclTableAndSecurityDescriptorProperty.anonymousNoRights,
				FolderSecurity.AclTableAndSecurityDescriptorProperty.everyoneNoRights
			});

			// Token: 0x0400334E RID: 13134
			private static byte[] defaultForPublicFolders = FolderSecurity.AclTableAndSecurityDescriptorProperty.GetBlobForSecurityDescriptor(new FolderSecurity.SecurityIdentifierAndFolderRights[]
			{
				FolderSecurity.AclTableAndSecurityDescriptorProperty.anonymousNoRights,
				new FolderSecurity.SecurityIdentifierAndFolderRights(FolderSecurity.AclTableAndSecurityDescriptorProperty.everyoneSID, FolderSecurity.ExchangeFolderRights.Author, FolderSecurity.SecurityIdentifierType.User)
			});

			// Token: 0x0400334F RID: 13135
			private static byte[] defaultForInternalSubmissionPublicFolder = FolderSecurity.AclTableAndSecurityDescriptorProperty.GetBlobForSecurityDescriptor(new FolderSecurity.SecurityIdentifierAndFolderRights[]
			{
				FolderSecurity.AclTableAndSecurityDescriptorProperty.anonymousNoRights,
				new FolderSecurity.SecurityIdentifierAndFolderRights(FolderSecurity.AclTableAndSecurityDescriptorProperty.everyoneSID, FolderSecurity.ExchangeFolderRights.Create | FolderSecurity.ExchangeFolderRights.EditOwned | FolderSecurity.ExchangeFolderRights.DeleteOwned | FolderSecurity.ExchangeFolderRights.Visible, FolderSecurity.SecurityIdentifierType.User)
			});

			// Token: 0x04003350 RID: 13136
			private SecurityDescriptor securityDescriptor;

			// Token: 0x04003351 RID: 13137
			private SecurityDescriptor freeBusySecurityDescriptor;

			// Token: 0x04003352 RID: 13138
			private ArraySegment<byte> serializedAclTable;

			// Token: 0x04003353 RID: 13139
			private Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> securityIdentifierToTypeMap;

			// Token: 0x02000AAA RID: 2730
			private struct Segment
			{
				// Token: 0x06003ABF RID: 15039 RVA: 0x00096A89 File Offset: 0x00094C89
				private Segment(FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType type, int length)
				{
					this.type = type;
					this.length = length;
				}

				// Token: 0x06003AC0 RID: 15040 RVA: 0x00096A9C File Offset: 0x00094C9C
				public static FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment Parse(BinaryReader reader)
				{
					FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType segmentType = (FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType)reader.ReadInt32();
					int num = reader.ReadInt32();
					if (num < 0)
					{
						throw new ArgumentException("Segment length cannot be negative");
					}
					return new FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment(segmentType, num);
				}

				// Token: 0x06003AC1 RID: 15041 RVA: 0x00096ACD File Offset: 0x00094CCD
				public T Parse<T>(BinaryReader reader, Func<BinaryReader, T> parser)
				{
					return parser(reader);
				}

				// Token: 0x06003AC2 RID: 15042 RVA: 0x00096AD8 File Offset: 0x00094CD8
				public static void Serialize<T>(BinaryWriter writer, FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType segmentType, T contentObject, FolderSecurity.AclTableAndSecurityDescriptorProperty.Segment.SerializerDelegate<T> serializer)
				{
					writer.Write((int)segmentType);
					long position = writer.BaseStream.Position;
					writer.Write(0);
					long position2 = writer.BaseStream.Position;
					serializer(writer, contentObject);
					long position3 = writer.BaseStream.Position;
					writer.BaseStream.Position = position;
					writer.Write((int)(position3 - position2));
					writer.BaseStream.Position = position3;
				}

				// Token: 0x06003AC3 RID: 15043 RVA: 0x00096B41 File Offset: 0x00094D41
				public void SkipData(BinaryReader reader)
				{
					reader.BaseStream.Position += (long)this.length;
				}

				// Token: 0x17000E9F RID: 3743
				// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x00096B5C File Offset: 0x00094D5C
				public FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType SegmentType
				{
					get
					{
						return this.type;
					}
				}

				// Token: 0x04003356 RID: 13142
				private readonly FolderSecurity.AclTableAndSecurityDescriptorProperty.SegmentType type;

				// Token: 0x04003357 RID: 13143
				private readonly int length;

				// Token: 0x02000AAB RID: 2731
				// (Invoke) Token: 0x06003AC6 RID: 15046
				public delegate void SerializerDelegate<T>(BinaryWriter writer, T contentObject);
			}

			// Token: 0x02000AAC RID: 2732
			private enum SegmentType
			{
				// Token: 0x04003359 RID: 13145
				Table,
				// Token: 0x0400335A RID: 13146
				SecurityIdentifierMap,
				// Token: 0x0400335B RID: 13147
				SecurityDescriptor,
				// Token: 0x0400335C RID: 13148
				FreeBusySecurityDescriptor
			}
		}

		// Token: 0x02000AAD RID: 2733
		public class SecurityIdentifierAndFolderRights
		{
			// Token: 0x06003AC9 RID: 15049 RVA: 0x00096B64 File Offset: 0x00094D64
			public SecurityIdentifierAndFolderRights(SecurityIdentifier securityIdentifier, FolderSecurity.ExchangeFolderRights allowRights, FolderSecurity.ExchangeFolderRights denyRights, FolderSecurity.SecurityIdentifierType securityIdentifierType)
			{
				this.securityIdentifier = securityIdentifier;
				this.allowRights = allowRights;
				this.denyRights = denyRights;
				this.securityIdentifierType = securityIdentifierType;
			}

			// Token: 0x06003ACA RID: 15050 RVA: 0x00096B89 File Offset: 0x00094D89
			public SecurityIdentifierAndFolderRights(SecurityIdentifier securityIdentifier, FolderSecurity.ExchangeFolderRights allowRights, FolderSecurity.SecurityIdentifierType securityIdentifierType) : this(securityIdentifier, allowRights, FolderSecurity.ExchangeFolderRights.AllFolderRights & ~allowRights, securityIdentifierType)
			{
			}

			// Token: 0x17000EA0 RID: 3744
			// (get) Token: 0x06003ACB RID: 15051 RVA: 0x00096B9C File Offset: 0x00094D9C
			public SecurityIdentifier SecurityIdentifier
			{
				get
				{
					return this.securityIdentifier;
				}
			}

			// Token: 0x17000EA1 RID: 3745
			// (get) Token: 0x06003ACC RID: 15052 RVA: 0x00096BA4 File Offset: 0x00094DA4
			// (set) Token: 0x06003ACD RID: 15053 RVA: 0x00096BAC File Offset: 0x00094DAC
			public FolderSecurity.ExchangeFolderRights AllowRights
			{
				get
				{
					return this.allowRights;
				}
				set
				{
					this.allowRights = value;
				}
			}

			// Token: 0x17000EA2 RID: 3746
			// (get) Token: 0x06003ACE RID: 15054 RVA: 0x00096BB5 File Offset: 0x00094DB5
			// (set) Token: 0x06003ACF RID: 15055 RVA: 0x00096BBD File Offset: 0x00094DBD
			public FolderSecurity.ExchangeFolderRights DenyRights
			{
				get
				{
					return this.denyRights;
				}
				set
				{
					this.denyRights = value;
				}
			}

			// Token: 0x17000EA3 RID: 3747
			// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x00096BC6 File Offset: 0x00094DC6
			public FolderSecurity.SecurityIdentifierType SecurityIdentifierType
			{
				get
				{
					return this.securityIdentifierType;
				}
			}

			// Token: 0x0400335D RID: 13149
			private readonly SecurityIdentifier securityIdentifier;

			// Token: 0x0400335E RID: 13150
			private FolderSecurity.ExchangeFolderRights allowRights;

			// Token: 0x0400335F RID: 13151
			private FolderSecurity.ExchangeFolderRights denyRights;

			// Token: 0x04003360 RID: 13152
			private FolderSecurity.SecurityIdentifierType securityIdentifierType;
		}

		// Token: 0x02000AAE RID: 2734
		public class AnnotatedAceList
		{
			// Token: 0x06003AD1 RID: 15057 RVA: 0x00096BD0 File Offset: 0x00094DD0
			public AnnotatedAceList(RawSecurityDescriptor securityDescriptor, RawSecurityDescriptor freeBusySecurityDescriptor, Func<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> securityIdentifierTypeResolver)
			{
				this.securityIdentifierTypeResolver = securityIdentifierTypeResolver;
				if (securityDescriptor.DiscretionaryAcl != null)
				{
					this.aceEntries = new List<FolderSecurity.AnnotatedAceList.AnnotatedAce>(securityDescriptor.DiscretionaryAcl.Count);
					foreach (GenericAce genericAce in securityDescriptor.DiscretionaryAcl)
					{
						if (genericAce.AceType == AceType.AccessAllowedObject || genericAce.AceType == AceType.AccessDeniedObject)
						{
							this.objectAceFound = true;
						}
						else
						{
							KnownAce knownAce = genericAce as KnownAce;
							if (knownAce == null)
							{
								this.nonCanonicalAceFound = true;
							}
							else if (!knownAce.SecurityIdentifier.IsWellKnown(WellKnownSidType.SelfSid))
							{
								FolderSecurity.AceTarget aceTarget;
								FolderSecurity.SecurityIdentifierType securityIdentifierType;
								FolderSecurity.AnnotatedAceList.AceCanonicalType aceCanonicalType = this.GetAceCanonicalType(knownAce, out aceTarget, out securityIdentifierType);
								if (aceCanonicalType == FolderSecurity.AnnotatedAceList.AceCanonicalType.Invalid)
								{
									this.nonCanonicalAceFound = true;
								}
								else
								{
									this.aceEntries.Add(new FolderSecurity.AnnotatedAceList.AnnotatedAce(knownAce, aceCanonicalType, aceTarget, securityIdentifierType));
								}
							}
						}
					}
				}
				else
				{
					this.aceEntries = new List<FolderSecurity.AnnotatedAceList.AnnotatedAce>();
				}
				if (freeBusySecurityDescriptor != null && freeBusySecurityDescriptor.DiscretionaryAcl != null)
				{
					foreach (GenericAce genericAce2 in freeBusySecurityDescriptor.DiscretionaryAcl)
					{
						KnownAce knownAce2 = genericAce2 as KnownAce;
						if (!(knownAce2 == null) && !knownAce2.SecurityIdentifier.IsWellKnown(WellKnownSidType.SelfSid))
						{
							FolderSecurity.SecurityIdentifierType securityIdentifierType2;
							FolderSecurity.AnnotatedAceList.AceCanonicalType aceCanonicalTypeForTarget = this.GetAceCanonicalTypeForTarget(knownAce2, FolderSecurity.AceTarget.FreeBusy, out securityIdentifierType2);
							if (aceCanonicalTypeForTarget != FolderSecurity.AnnotatedAceList.AceCanonicalType.Invalid)
							{
								this.aceEntries.Add(new FolderSecurity.AnnotatedAceList.AnnotatedAce(knownAce2, aceCanonicalTypeForTarget, FolderSecurity.AceTarget.FreeBusy, securityIdentifierType2));
							}
						}
					}
				}
			}

			// Token: 0x06003AD2 RID: 15058 RVA: 0x00096D5C File Offset: 0x00094F5C
			public IList<FolderSecurity.SecurityIdentifierAndFolderRights> GetSecurityIdentifierAndRightsList()
			{
				List<FolderSecurity.SecurityIdentifierAndFolderRights> list = new List<FolderSecurity.SecurityIdentifierAndFolderRights>(this.aceEntries.Count);
				using (IEnumerator<FolderSecurity.AnnotatedAceList.AnnotatedAce> enumerator = this.aceEntries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						FolderSecurity.AnnotatedAceList.AnnotatedAce ace = enumerator.Current;
						FolderSecurity.AnnotatedAceList.AnnotatedAce ace9 = ace;
						FolderSecurity.ExchangeSecurityDescriptorFolderRights accessMask = (FolderSecurity.ExchangeSecurityDescriptorFolderRights)ace9.Ace.AccessMask;
						FolderSecurity.ExchangeSecurityDescriptorFolderRights sdRights = accessMask;
						FolderSecurity.AnnotatedAceList.AnnotatedAce ace2 = ace;
						FolderSecurity.ExchangeFolderRights exchangeFolderRights = FolderSecurity.FolderRightsFromSecurityDescriptorRights(sdRights, ace2.Target);
						int num = list.FindIndex(delegate(FolderSecurity.SecurityIdentifierAndFolderRights entry)
						{
							SecurityIdentifier securityIdentifier2 = entry.SecurityIdentifier;
							FolderSecurity.AnnotatedAceList.AnnotatedAce ace8 = ace;
							return securityIdentifier2 == ace8.Ace.SecurityIdentifier;
						});
						if (num != -1)
						{
							FolderSecurity.AnnotatedAceList.AnnotatedAce ace3 = ace;
							if (ace3.IsAllowAce)
							{
								list[num].AllowRights |= (exchangeFolderRights & ~list[num].DenyRights);
							}
							else
							{
								list[num].DenyRights |= (exchangeFolderRights & ~list[num].AllowRights);
							}
						}
						else
						{
							List<FolderSecurity.SecurityIdentifierAndFolderRights> list2 = list;
							FolderSecurity.AnnotatedAceList.AnnotatedAce ace4 = ace;
							SecurityIdentifier securityIdentifier = ace4.Ace.SecurityIdentifier;
							FolderSecurity.AnnotatedAceList.AnnotatedAce ace5 = ace;
							FolderSecurity.ExchangeFolderRights allowRights = ace5.IsAllowAce ? exchangeFolderRights : FolderSecurity.ExchangeFolderRights.None;
							FolderSecurity.AnnotatedAceList.AnnotatedAce ace6 = ace;
							FolderSecurity.ExchangeFolderRights denyRights = ace6.IsAllowAce ? FolderSecurity.ExchangeFolderRights.None : exchangeFolderRights;
							FolderSecurity.AnnotatedAceList.AnnotatedAce ace7 = ace;
							list2.Add(new FolderSecurity.SecurityIdentifierAndFolderRights(securityIdentifier, allowRights, denyRights, ace7.SecurityIdentifierType));
						}
					}
				}
				return list;
			}

			// Token: 0x06003AD3 RID: 15059 RVA: 0x00096EFC File Offset: 0x000950FC
			public bool IsCanonical(out string errorInformation)
			{
				if (this.objectAceFound || this.nonCanonicalAceFound)
				{
					errorInformation = this.CreateErrorInformation((LID)52352U, new int[0]);
					return false;
				}
				Dictionary<SecurityIdentifier, int> dictionary = new Dictionary<SecurityIdentifier, int>(this.aceEntries.Count);
				Dictionary<SecurityIdentifier, int> dictionary2 = new Dictionary<SecurityIdentifier, int>(this.aceEntries.Count);
				if (!this.IsCanonicalForTarget(FolderSecurity.AceTarget.Folder, dictionary, dictionary2, out errorInformation))
				{
					return false;
				}
				if (!this.IsCanonicalForTarget(FolderSecurity.AceTarget.Message, dictionary, dictionary2, out errorInformation))
				{
					return false;
				}
				if (!dictionary2.Values.Any((int rights) => !FolderSecurity.AnnotatedAceList.HasFullRights(rights, FolderSecurity.AceTarget.Folder)))
				{
					if (!dictionary.Values.Any((int rights) => !FolderSecurity.AnnotatedAceList.HasFullRights(rights, FolderSecurity.AceTarget.Message)))
					{
						return true;
					}
				}
				errorInformation = this.CreateErrorInformation((LID)42112U, new int[0]);
				return false;
			}

			// Token: 0x06003AD4 RID: 15060 RVA: 0x00096FE0 File Offset: 0x000951E0
			private bool IsCanonicalForTarget(FolderSecurity.AceTarget target, Dictionary<SecurityIdentifier, int> accumulativeMessageSidRights, Dictionary<SecurityIdentifier, int> accumulativeFolderSidRights, out string errorInformation)
			{
				FolderSecurity.AnnotatedAceList.AceCanonicalType aceCanonicalType = FolderSecurity.AnnotatedAceList.AceCanonicalType.Invalid;
				bool flag = false;
				FolderSecurity.AnnotatedAceList.AnnotatedAce? annotatedAce = null;
				for (int i = 0; i < this.aceEntries.Count; i++)
				{
					FolderSecurity.AnnotatedAceList.AnnotatedAce value = this.aceEntries[i];
					if (value.Target == target && !value.Ace.SecurityIdentifier.IsWellKnown(WellKnownSidType.AnonymousSid))
					{
						FolderSecurity.AnnotatedAceList.AceCanonicalType canonicalType = value.CanonicalType;
						bool[][] array;
						int num;
						if (!flag)
						{
							if (canonicalType == FolderSecurity.AnnotatedAceList.AceCanonicalType.GroupAllow || canonicalType == FolderSecurity.AnnotatedAceList.AceCanonicalType.GroupDeny || (i < this.aceEntries.Count - 1 && canonicalType == FolderSecurity.AnnotatedAceList.AceCanonicalType.UnknownAllowPartial && (this.aceEntries[i + 1].CanonicalType == FolderSecurity.AnnotatedAceList.AceCanonicalType.UnknownAllowFull || this.aceEntries[i + 1].CanonicalType == FolderSecurity.AnnotatedAceList.AceCanonicalType.UnknownAllowPartial)))
							{
								flag = true;
								array = FolderSecurity.AnnotatedAceList.userToGroupTransition;
								num = 0;
							}
							else
							{
								array = FolderSecurity.AnnotatedAceList.userSectionTransitions;
								num = 1;
							}
						}
						else
						{
							array = FolderSecurity.AnnotatedAceList.groupSectionTransitions;
							num = 2;
						}
						if (!array[(int)aceCanonicalType][(int)canonicalType])
						{
							errorInformation = this.CreateErrorInformation((LID)46208U, new int[]
							{
								i,
								num,
								(int)aceCanonicalType,
								(int)canonicalType
							});
							return false;
						}
						aceCanonicalType = canonicalType;
						switch (canonicalType)
						{
						case FolderSecurity.AnnotatedAceList.AceCanonicalType.UserAllowFull:
						case FolderSecurity.AnnotatedAceList.AceCanonicalType.UserAllowPartial:
							annotatedAce = new FolderSecurity.AnnotatedAceList.AnnotatedAce?(value);
							break;
						case FolderSecurity.AnnotatedAceList.AceCanonicalType.UserDenyFull:
							goto IL_1C9;
						case FolderSecurity.AnnotatedAceList.AceCanonicalType.UserDenyPartial:
							if (annotatedAce == null || annotatedAce.Value.Ace.SecurityIdentifier != value.Ace.SecurityIdentifier || !FolderSecurity.AnnotatedAceList.HasFullRights(annotatedAce.Value.Ace.AccessMask | value.Ace.AccessMask, value.Target))
							{
								errorInformation = this.CreateErrorInformation((LID)54400U, new int[]
								{
									i
								});
								return false;
							}
							annotatedAce = null;
							break;
						default:
							goto IL_1C9;
						}
						IL_1D1:
						if (!value.Ace.SecurityIdentifier.IsWellKnown(WellKnownSidType.WorldSid))
						{
							if (!accumulativeFolderSidRights.ContainsKey(value.Ace.SecurityIdentifier))
							{
								accumulativeFolderSidRights[value.Ace.SecurityIdentifier] = 0;
							}
							if (!accumulativeMessageSidRights.ContainsKey(value.Ace.SecurityIdentifier))
							{
								accumulativeMessageSidRights[value.Ace.SecurityIdentifier] = 0;
							}
							Dictionary<SecurityIdentifier, int> dictionary = (target == FolderSecurity.AceTarget.Folder) ? accumulativeFolderSidRights : accumulativeMessageSidRights;
							dictionary[value.Ace.SecurityIdentifier] = (dictionary[value.Ace.SecurityIdentifier] | value.Ace.AccessMask);
							goto IL_272;
						}
						goto IL_272;
						IL_1C9:
						annotatedAce = null;
						goto IL_1D1;
					}
					IL_272:;
				}
				errorInformation = string.Empty;
				return true;
			}

			// Token: 0x06003AD5 RID: 15061 RVA: 0x0009727D File Offset: 0x0009547D
			public static RawAcl BuildFolderCanonicalAceList(IList<FolderSecurity.SecurityIdentifierAndFolderRights> sidAndRightsList)
			{
				return FolderSecurity.AnnotatedAceList.BuildCanonicalAceList(sidAndRightsList, new FolderSecurity.AnnotatedAceList.AppendUserAceToAclDelegate(FolderSecurity.AnnotatedAceList.AppendUserAceToFolderAcl), new FolderSecurity.AnnotatedAceList.AppendGroupAceToAclDelegate(FolderSecurity.AnnotatedAceList.AppendGroupAceToFolderAcl));
			}

			// Token: 0x06003AD6 RID: 15062 RVA: 0x0009729D File Offset: 0x0009549D
			public static RawAcl BuildFreeBusyCanonicalAceList(IList<FolderSecurity.SecurityIdentifierAndFolderRights> sidAndRightsList)
			{
				return FolderSecurity.AnnotatedAceList.BuildCanonicalAceList(sidAndRightsList, new FolderSecurity.AnnotatedAceList.AppendUserAceToAclDelegate(FolderSecurity.AnnotatedAceList.AppendUserAceToFreeBusyAcl), new FolderSecurity.AnnotatedAceList.AppendGroupAceToAclDelegate(FolderSecurity.AnnotatedAceList.AppendGroupAceToFreeBusyAcl));
			}

			// Token: 0x06003AD7 RID: 15063 RVA: 0x000972C0 File Offset: 0x000954C0
			private static RawAcl BuildCanonicalAceList(IList<FolderSecurity.SecurityIdentifierAndFolderRights> sidAndRightsList, FolderSecurity.AnnotatedAceList.AppendUserAceToAclDelegate appendUserAceToAclDelegate, FolderSecurity.AnnotatedAceList.AppendGroupAceToAclDelegate appendGroupAceToAclDelegate)
			{
				RawAcl rawAcl = new RawAcl(2, sidAndRightsList.Count * 4);
				List<int> list = new List<int>(sidAndRightsList.Count);
				FolderSecurity.SecurityIdentifierAndFolderRights securityIdentifierAndFolderRights = null;
				FolderSecurity.SecurityIdentifierAndFolderRights securityIdentifierAndFolderRights2 = null;
				for (int i = 0; i < sidAndRightsList.Count; i++)
				{
					FolderSecurity.SecurityIdentifierAndFolderRights securityIdentifierAndFolderRights3 = sidAndRightsList[i];
					switch (securityIdentifierAndFolderRights3.SecurityIdentifierType)
					{
					case FolderSecurity.SecurityIdentifierType.Unknown:
					case FolderSecurity.SecurityIdentifierType.User:
						if (securityIdentifierAndFolderRights3.SecurityIdentifier.IsWellKnown(WellKnownSidType.WorldSid))
						{
							securityIdentifierAndFolderRights = securityIdentifierAndFolderRights3;
						}
						else if (securityIdentifierAndFolderRights3.SecurityIdentifier.IsWellKnown(WellKnownSidType.AnonymousSid))
						{
							securityIdentifierAndFolderRights2 = securityIdentifierAndFolderRights3;
						}
						else
						{
							appendUserAceToAclDelegate(rawAcl, securityIdentifierAndFolderRights3, true);
						}
						break;
					case FolderSecurity.SecurityIdentifierType.Group:
						list.Add(i);
						break;
					}
				}
				foreach (int index in list)
				{
					appendGroupAceToAclDelegate(rawAcl, true, sidAndRightsList[index]);
				}
				foreach (int index2 in list)
				{
					appendGroupAceToAclDelegate(rawAcl, false, sidAndRightsList[index2]);
				}
				if (securityIdentifierAndFolderRights2 != null)
				{
					appendUserAceToAclDelegate(rawAcl, securityIdentifierAndFolderRights2, true);
				}
				if (securityIdentifierAndFolderRights != null)
				{
					appendUserAceToAclDelegate(rawAcl, securityIdentifierAndFolderRights, false);
				}
				return rawAcl;
			}

			// Token: 0x06003AD8 RID: 15064 RVA: 0x00097414 File Offset: 0x00095614
			private static void AppendGroupAceToFolderAcl(RawAcl acl, bool allowAccess, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights)
			{
				FolderSecurity.AnnotatedAceList.AppendAceToAcl(acl, allowAccess, FolderSecurity.AceTarget.Folder, sidAndRights);
				FolderSecurity.AnnotatedAceList.AppendAceToAcl(acl, allowAccess, FolderSecurity.AceTarget.Message, sidAndRights);
			}

			// Token: 0x06003AD9 RID: 15065 RVA: 0x00097428 File Offset: 0x00095628
			private static void AppendUserAceToFolderAcl(RawAcl acl, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights, bool addDenyAce)
			{
				FolderSecurity.AnnotatedAceList.InsertUserAceToFolderAcl(acl, acl.Count, sidAndRights, addDenyAce);
			}

			// Token: 0x06003ADA RID: 15066 RVA: 0x00097438 File Offset: 0x00095638
			internal static void InsertUserAceToFolderAcl(RawAcl acl, int insertIndex, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights, bool addDenyAce)
			{
				int num = insertIndex;
				if (FolderSecurity.AnnotatedAceList.InsertAceToAcl(acl, num, true, FolderSecurity.AceTarget.Folder, sidAndRights))
				{
					num++;
				}
				if (addDenyAce && FolderSecurity.AnnotatedAceList.InsertAceToAcl(acl, num, false, FolderSecurity.AceTarget.Folder, sidAndRights))
				{
					num++;
				}
				if (FolderSecurity.AnnotatedAceList.InsertAceToAcl(acl, num, true, FolderSecurity.AceTarget.Message, sidAndRights))
				{
					num++;
				}
				if (addDenyAce && FolderSecurity.AnnotatedAceList.InsertAceToAcl(acl, num, false, FolderSecurity.AceTarget.Message, sidAndRights))
				{
					num++;
				}
			}

			// Token: 0x06003ADB RID: 15067 RVA: 0x00097490 File Offset: 0x00095690
			internal static void RemoveAcesFromAcl(RawAcl acl, SecurityIdentifier sid)
			{
				for (int i = acl.Count - 1; i >= 0; i--)
				{
					KnownAce knownAce = acl[i] as KnownAce;
					if (knownAce != null && knownAce.SecurityIdentifier == sid)
					{
						acl.RemoveAce(i);
					}
				}
			}

			// Token: 0x06003ADC RID: 15068 RVA: 0x000974DB File Offset: 0x000956DB
			private static void AppendGroupAceToFreeBusyAcl(RawAcl acl, bool allowAccess, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights)
			{
				FolderSecurity.AnnotatedAceList.AppendAceToAcl(acl, allowAccess, FolderSecurity.AceTarget.FreeBusy, sidAndRights);
			}

			// Token: 0x06003ADD RID: 15069 RVA: 0x000974E6 File Offset: 0x000956E6
			private static void AppendUserAceToFreeBusyAcl(RawAcl acl, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights, bool addDenyAce)
			{
				FolderSecurity.AnnotatedAceList.AppendAceToAcl(acl, true, FolderSecurity.AceTarget.FreeBusy, sidAndRights);
				if (addDenyAce)
				{
					FolderSecurity.AnnotatedAceList.AppendAceToAcl(acl, false, FolderSecurity.AceTarget.FreeBusy, sidAndRights);
				}
			}

			// Token: 0x06003ADE RID: 15070 RVA: 0x000974FD File Offset: 0x000956FD
			private static void AppendAceToAcl(RawAcl acl, bool allowAccess, FolderSecurity.AceTarget aceTarget, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights)
			{
				FolderSecurity.AnnotatedAceList.InsertAceToAcl(acl, acl.Count, allowAccess, aceTarget, sidAndRights);
			}

			// Token: 0x06003ADF RID: 15071 RVA: 0x00097510 File Offset: 0x00095710
			private static bool InsertAceToAcl(RawAcl acl, int insertIndex, bool allowAccess, FolderSecurity.AceTarget aceTarget, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights)
			{
				FolderSecurity.ExchangeSecurityDescriptorFolderRights exchangeSecurityDescriptorFolderRights = FolderSecurity.SecurityDescriptorRightsFromFolderRights(sidAndRights.AllowRights, aceTarget);
				FolderSecurity.ExchangeSecurityDescriptorFolderRights exchangeSecurityDescriptorFolderRights2;
				if (allowAccess)
				{
					exchangeSecurityDescriptorFolderRights2 = exchangeSecurityDescriptorFolderRights;
				}
				else
				{
					FolderSecurity.ExchangeSecurityDescriptorFolderRights exchangeSecurityDescriptorFolderRights3 = FolderSecurity.SecurityDescriptorRightsFromFolderRights(sidAndRights.DenyRights, aceTarget);
					FolderSecurity.ExchangeSecurityDescriptorFolderRights exchangeSecurityDescriptorFolderRights4 = exchangeSecurityDescriptorFolderRights & exchangeSecurityDescriptorFolderRights3;
					exchangeSecurityDescriptorFolderRights3 &= ~exchangeSecurityDescriptorFolderRights4;
					exchangeSecurityDescriptorFolderRights2 = exchangeSecurityDescriptorFolderRights3;
				}
				if (exchangeSecurityDescriptorFolderRights2 == FolderSecurity.ExchangeSecurityDescriptorFolderRights.None)
				{
					return false;
				}
				acl.InsertAce(insertIndex, new CommonAce(FolderSecurity.AnnotatedAceList.GetAceFlagsForTarget(aceTarget), allowAccess ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, (int)exchangeSecurityDescriptorFolderRights2, sidAndRights.SecurityIdentifier, false, null));
				return true;
			}

			// Token: 0x06003AE0 RID: 15072 RVA: 0x00097574 File Offset: 0x00095774
			private static bool IsFolderAce(GenericAce ace)
			{
				return (ace.AceFlags & FolderSecurity.AnnotatedAceList.aceFlagsMask) == FolderSecurity.AnnotatedAceList.GetAceFlagsForTarget(FolderSecurity.AceTarget.Folder);
			}

			// Token: 0x06003AE1 RID: 15073 RVA: 0x0009758B File Offset: 0x0009578B
			private static bool IsMessageAce(GenericAce ace)
			{
				return (ace.AceFlags & FolderSecurity.AnnotatedAceList.aceFlagsMask) == FolderSecurity.AnnotatedAceList.GetAceFlagsForTarget(FolderSecurity.AceTarget.Message);
			}

			// Token: 0x06003AE2 RID: 15074 RVA: 0x000975A2 File Offset: 0x000957A2
			private static AceFlags GetAceFlagsForTarget(FolderSecurity.AceTarget aceTarget)
			{
				if (aceTarget != FolderSecurity.AceTarget.Folder && aceTarget != FolderSecurity.AceTarget.FreeBusy)
				{
					return AceFlags.ObjectInherit | AceFlags.InheritOnly;
				}
				return AceFlags.ContainerInherit;
			}

			// Token: 0x06003AE3 RID: 15075 RVA: 0x000975B0 File Offset: 0x000957B0
			private static bool HasFullRights(int accessMask, FolderSecurity.AceTarget aceTarget)
			{
				switch (aceTarget)
				{
				case FolderSecurity.AceTarget.Folder:
					return (accessMask & -32869) == 2050459;
				case FolderSecurity.AceTarget.Message:
					return (accessMask & -32869) == 2035611;
				case FolderSecurity.AceTarget.FreeBusy:
					return accessMask == 3;
				default:
					return false;
				}
			}

			// Token: 0x06003AE4 RID: 15076 RVA: 0x000975FC File Offset: 0x000957FC
			internal string CreateErrorInformation(LID errorLocation, params int[] errorParameters)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (FolderSecurity.AnnotatedAceList.AnnotatedAce annotatedAce in this.aceEntries)
				{
					stringBuilder.AppendFormat("({0};{1};{2};{3:X};{4};{5})", new object[]
					{
						annotatedAce.IsAllowAce ? 'A' : 'D',
						annotatedAce.Ace.SecurityIdentifier,
						annotatedAce.Target,
						annotatedAce.Ace.AccessMask,
						annotatedAce.CanonicalType,
						annotatedAce.SecurityIdentifierType
					});
				}
				stringBuilder.Append(";EL:");
				stringBuilder.Append(errorLocation.Value);
				foreach (int value in errorParameters)
				{
					stringBuilder.Append(';');
					stringBuilder.Append(value);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06003AE5 RID: 15077 RVA: 0x00097720 File Offset: 0x00095920
			private FolderSecurity.AnnotatedAceList.AceCanonicalType GetAceCanonicalType(KnownAce ace, out FolderSecurity.AceTarget aceTarget, out FolderSecurity.SecurityIdentifierType securityIdentifierType)
			{
				aceTarget = FolderSecurity.AceTarget.Folder;
				securityIdentifierType = FolderSecurity.SecurityIdentifierType.User;
				if (FolderSecurity.AnnotatedAceList.IsFolderAce(ace))
				{
					aceTarget = FolderSecurity.AceTarget.Folder;
				}
				else
				{
					if (!FolderSecurity.AnnotatedAceList.IsMessageAce(ace))
					{
						return FolderSecurity.AnnotatedAceList.AceCanonicalType.Invalid;
					}
					aceTarget = FolderSecurity.AceTarget.Message;
				}
				return this.GetAceCanonicalTypeForTarget(ace, aceTarget, out securityIdentifierType);
			}

			// Token: 0x06003AE6 RID: 15078 RVA: 0x00097750 File Offset: 0x00095950
			private FolderSecurity.AnnotatedAceList.AceCanonicalType GetAceCanonicalTypeForTarget(KnownAce ace, FolderSecurity.AceTarget aceTarget, out FolderSecurity.SecurityIdentifierType securityIdentifierType)
			{
				securityIdentifierType = FolderSecurity.SecurityIdentifierType.User;
				bool flag;
				if (ace.AceType == AceType.AccessAllowed)
				{
					flag = true;
				}
				else
				{
					if (ace.AceType != AceType.AccessDenied)
					{
						return FolderSecurity.AnnotatedAceList.AceCanonicalType.Invalid;
					}
					flag = false;
				}
				if (ace.SecurityIdentifier.IsWellKnown(WellKnownSidType.WorldSid))
				{
					if (!flag)
					{
						return FolderSecurity.AnnotatedAceList.AceCanonicalType.Invalid;
					}
					return FolderSecurity.AnnotatedAceList.AceCanonicalType.Everyone;
				}
				else if (ace.SecurityIdentifier.IsWellKnown(WellKnownSidType.AnonymousSid))
				{
					if (!flag)
					{
						if (!FolderSecurity.AnnotatedAceList.HasFullRights(ace.AccessMask, aceTarget))
						{
							return FolderSecurity.AnnotatedAceList.AceCanonicalType.UserDenyPartial;
						}
						return FolderSecurity.AnnotatedAceList.AceCanonicalType.UserDenyFull;
					}
					else
					{
						if (!FolderSecurity.AnnotatedAceList.HasFullRights(ace.AccessMask, aceTarget))
						{
							return FolderSecurity.AnnotatedAceList.AceCanonicalType.UserAllowPartial;
						}
						return FolderSecurity.AnnotatedAceList.AceCanonicalType.UserAllowFull;
					}
				}
				else
				{
					securityIdentifierType = this.securityIdentifierTypeResolver(ace.SecurityIdentifier);
					switch (securityIdentifierType)
					{
					case FolderSecurity.SecurityIdentifierType.Unknown:
						if (!flag)
						{
							if (!FolderSecurity.AnnotatedAceList.HasFullRights(ace.AccessMask, aceTarget))
							{
								return FolderSecurity.AnnotatedAceList.AceCanonicalType.UnknownDenyPartial;
							}
							return FolderSecurity.AnnotatedAceList.AceCanonicalType.UnknownDenyFull;
						}
						else
						{
							if (!FolderSecurity.AnnotatedAceList.HasFullRights(ace.AccessMask, aceTarget))
							{
								return FolderSecurity.AnnotatedAceList.AceCanonicalType.UnknownAllowPartial;
							}
							return FolderSecurity.AnnotatedAceList.AceCanonicalType.UnknownAllowFull;
						}
						break;
					case FolderSecurity.SecurityIdentifierType.User:
						if (!flag)
						{
							if (!FolderSecurity.AnnotatedAceList.HasFullRights(ace.AccessMask, aceTarget))
							{
								return FolderSecurity.AnnotatedAceList.AceCanonicalType.UserDenyPartial;
							}
							return FolderSecurity.AnnotatedAceList.AceCanonicalType.UserDenyFull;
						}
						else
						{
							if (!FolderSecurity.AnnotatedAceList.HasFullRights(ace.AccessMask, aceTarget))
							{
								return FolderSecurity.AnnotatedAceList.AceCanonicalType.UserAllowPartial;
							}
							return FolderSecurity.AnnotatedAceList.AceCanonicalType.UserAllowFull;
						}
						break;
					case FolderSecurity.SecurityIdentifierType.Group:
						if (!flag)
						{
							return FolderSecurity.AnnotatedAceList.AceCanonicalType.GroupDeny;
						}
						return FolderSecurity.AnnotatedAceList.AceCanonicalType.GroupAllow;
					default:
						return FolderSecurity.AnnotatedAceList.AceCanonicalType.Invalid;
					}
				}
			}

			// Token: 0x06003AE9 RID: 15081 RVA: 0x0009796C File Offset: 0x00095B6C
			// Note: this type is marked as 'beforefieldinit'.
			static AnnotatedAceList()
			{
				bool[][] array = new bool[8][];
				array[0] = new bool[]
				{
					true,
					true,
					true,
					true,
					true,
					true,
					true,
					false,
					true,
					true,
					true,
					false
				};
				bool[][] array2 = array;
				int num = 1;
				bool[] array3 = new bool[12];
				array3[2] = true;
				array3[3] = true;
				array2[num] = array3;
				array[2] = new bool[]
				{
					true,
					true,
					true,
					false,
					true,
					true,
					true,
					false,
					true,
					true,
					true,
					false
				};
				array[3] = new bool[]
				{
					true,
					true,
					true,
					false,
					true,
					true,
					true,
					false,
					true,
					true,
					true,
					false
				};
				array[4] = new bool[]
				{
					false,
					false,
					false,
					false,
					true,
					true,
					true,
					false,
					true,
					true,
					true,
					true
				};
				array[5] = new bool[]
				{
					false,
					false,
					false,
					false,
					false,
					true,
					true,
					false,
					false,
					false,
					true,
					true
				};
				bool[][] array4 = array;
				int num2 = 6;
				bool[] array5 = new bool[12];
				array5[6] = true;
				array4[num2] = array5;
				array[7] = new bool[]
				{
					true,
					true,
					true,
					false,
					true,
					true,
					true,
					false,
					true,
					true,
					true,
					false
				};
				FolderSecurity.AnnotatedAceList.knowAceTransitions = array;
				FolderSecurity.AnnotatedAceList.userSectionUnknownAceTransition = new bool[][]
				{
					new bool[]
					{
						true,
						true,
						true,
						true,
						false,
						false,
						true,
						false,
						true,
						true,
						true,
						true
					},
					new bool[]
					{
						false,
						false,
						true,
						true,
						false,
						false,
						false,
						false,
						false,
						false,
						true,
						true
					},
					new bool[]
					{
						true,
						true,
						true,
						false,
						false,
						false,
						true,
						false,
						true,
						true,
						true,
						false
					},
					new bool[]
					{
						true,
						true,
						true,
						false,
						false,
						false,
						true,
						false,
						true,
						true,
						true,
						false
					}
				};
				FolderSecurity.AnnotatedAceList.groupSectionUnknownAceTransition = new bool[][]
				{
					new bool[]
					{
						false,
						false,
						false,
						false,
						true,
						true,
						true,
						false,
						true,
						true,
						true,
						true
					},
					new bool[]
					{
						false,
						false,
						false,
						false,
						true,
						true,
						false,
						false,
						true,
						true,
						true,
						true
					},
					new bool[]
					{
						false,
						false,
						false,
						false,
						false,
						true,
						true,
						false,
						false,
						false,
						true,
						true
					},
					new bool[]
					{
						false,
						false,
						false,
						false,
						false,
						true,
						true,
						false,
						false,
						false,
						true,
						true
					}
				};
				FolderSecurity.AnnotatedAceList.userToGroupUnknownAceTransition = new bool[][]
				{
					new bool[]
					{
						false,
						false,
						false,
						false,
						true,
						true,
						true,
						false,
						true,
						true,
						true,
						true
					},
					new bool[]
					{
						false,
						false,
						false,
						false,
						true,
						true,
						true,
						false,
						true,
						true,
						true,
						true
					},
					new bool[]
					{
						false,
						false,
						false,
						false,
						true,
						true,
						true,
						false,
						true,
						true,
						true,
						true
					},
					new bool[]
					{
						false,
						false,
						false,
						false,
						true,
						true,
						true,
						false,
						true,
						true,
						true,
						true
					}
				};
				FolderSecurity.AnnotatedAceList.userSectionTransitions = new List<bool[]>(FolderSecurity.AnnotatedAceList.knowAceTransitions).Concat(FolderSecurity.AnnotatedAceList.userSectionUnknownAceTransition).ToArray<bool[]>();
				FolderSecurity.AnnotatedAceList.userToGroupTransition = new List<bool[]>(FolderSecurity.AnnotatedAceList.knowAceTransitions).Concat(FolderSecurity.AnnotatedAceList.userToGroupUnknownAceTransition).ToArray<bool[]>();
				FolderSecurity.AnnotatedAceList.groupSectionTransitions = new List<bool[]>(FolderSecurity.AnnotatedAceList.knowAceTransitions).Concat(FolderSecurity.AnnotatedAceList.groupSectionUnknownAceTransition).ToArray<bool[]>();
			}

			// Token: 0x04003361 RID: 13153
			private static readonly AceFlags aceFlagsMask = AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.InheritOnly;

			// Token: 0x04003362 RID: 13154
			private readonly bool objectAceFound;

			// Token: 0x04003363 RID: 13155
			private readonly bool nonCanonicalAceFound;

			// Token: 0x04003364 RID: 13156
			private readonly IList<FolderSecurity.AnnotatedAceList.AnnotatedAce> aceEntries;

			// Token: 0x04003365 RID: 13157
			private readonly Func<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> securityIdentifierTypeResolver;

			// Token: 0x04003366 RID: 13158
			private static bool[][] knowAceTransitions;

			// Token: 0x04003367 RID: 13159
			private static bool[][] userSectionUnknownAceTransition;

			// Token: 0x04003368 RID: 13160
			private static bool[][] groupSectionUnknownAceTransition;

			// Token: 0x04003369 RID: 13161
			private static bool[][] userToGroupUnknownAceTransition;

			// Token: 0x0400336A RID: 13162
			private static bool[][] userSectionTransitions;

			// Token: 0x0400336B RID: 13163
			private static bool[][] userToGroupTransition;

			// Token: 0x0400336C RID: 13164
			private static bool[][] groupSectionTransitions;

			// Token: 0x02000AAF RID: 2735
			// (Invoke) Token: 0x06003AEB RID: 15083
			private delegate void AppendUserAceToAclDelegate(RawAcl acl, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights, bool addDenyAce);

			// Token: 0x02000AB0 RID: 2736
			// (Invoke) Token: 0x06003AEF RID: 15087
			private delegate void AppendGroupAceToAclDelegate(RawAcl acl, bool allowAccess, FolderSecurity.SecurityIdentifierAndFolderRights sidAndRights);

			// Token: 0x02000AB1 RID: 2737
			private struct AnnotatedAce
			{
				// Token: 0x06003AF2 RID: 15090 RVA: 0x00097BB8 File Offset: 0x00095DB8
				public AnnotatedAce(KnownAce ace, FolderSecurity.AnnotatedAceList.AceCanonicalType aceCanonicalType, FolderSecurity.AceTarget aceTarget, FolderSecurity.SecurityIdentifierType securityIdentifierType)
				{
					this.ace = ace;
					this.aceCanonicalType = aceCanonicalType;
					this.aceTarget = aceTarget;
					this.securityIdentifierType = securityIdentifierType;
				}

				// Token: 0x17000EA4 RID: 3748
				// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x00097BD7 File Offset: 0x00095DD7
				public KnownAce Ace
				{
					get
					{
						return this.ace;
					}
				}

				// Token: 0x17000EA5 RID: 3749
				// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x00097BDF File Offset: 0x00095DDF
				public FolderSecurity.AnnotatedAceList.AceCanonicalType CanonicalType
				{
					get
					{
						return this.aceCanonicalType;
					}
				}

				// Token: 0x17000EA6 RID: 3750
				// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x00097BE7 File Offset: 0x00095DE7
				public bool IsAllowAce
				{
					get
					{
						return this.ace.AceType == AceType.AccessAllowed;
					}
				}

				// Token: 0x17000EA7 RID: 3751
				// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x00097BF7 File Offset: 0x00095DF7
				public FolderSecurity.AceTarget Target
				{
					get
					{
						return this.aceTarget;
					}
				}

				// Token: 0x17000EA8 RID: 3752
				// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x00097BFF File Offset: 0x00095DFF
				public FolderSecurity.SecurityIdentifierType SecurityIdentifierType
				{
					get
					{
						return this.securityIdentifierType;
					}
				}

				// Token: 0x0400336F RID: 13167
				private readonly KnownAce ace;

				// Token: 0x04003370 RID: 13168
				private readonly FolderSecurity.AnnotatedAceList.AceCanonicalType aceCanonicalType;

				// Token: 0x04003371 RID: 13169
				private readonly FolderSecurity.AceTarget aceTarget;

				// Token: 0x04003372 RID: 13170
				private readonly FolderSecurity.SecurityIdentifierType securityIdentifierType;
			}

			// Token: 0x02000AB2 RID: 2738
			private enum AceCanonicalType
			{
				// Token: 0x04003374 RID: 13172
				UserAllowFull,
				// Token: 0x04003375 RID: 13173
				UserAllowPartial,
				// Token: 0x04003376 RID: 13174
				UserDenyFull,
				// Token: 0x04003377 RID: 13175
				UserDenyPartial,
				// Token: 0x04003378 RID: 13176
				GroupAllow,
				// Token: 0x04003379 RID: 13177
				GroupDeny,
				// Token: 0x0400337A RID: 13178
				Everyone,
				// Token: 0x0400337B RID: 13179
				LastCanonicalValue = 6,
				// Token: 0x0400337C RID: 13180
				Invalid,
				// Token: 0x0400337D RID: 13181
				UnknownAllowFull,
				// Token: 0x0400337E RID: 13182
				UnknownAllowPartial,
				// Token: 0x0400337F RID: 13183
				UnknownDenyFull,
				// Token: 0x04003380 RID: 13184
				UnknownDenyPartial
			}
		}
	}
}
