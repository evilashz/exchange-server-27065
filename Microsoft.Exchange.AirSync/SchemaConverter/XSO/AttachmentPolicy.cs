using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001ED RID: 493
	internal class AttachmentPolicy
	{
		// Token: 0x0600138D RID: 5005 RVA: 0x00070996 File Offset: 0x0006EB96
		internal AttachmentPolicy(string[] blockFileTypes, string[] blockMimeTypes, string[] forceSaveFileTypes, string[] forceSaveMimeTypes, string[] allowFileTypes, string[] allowMimeTypes, bool alwaysBlock, bool blockOnPublicComputers, AttachmentPolicy.Level treatUnknownTypeAs)
		{
			this.alwaysBlock = alwaysBlock;
			this.blockOnPublicComputers = blockOnPublicComputers;
			this.treatUnknownTypeAs = treatUnknownTypeAs;
			AttachmentPolicy.LoadDictionary(out this.fileTypeLevels, blockFileTypes, forceSaveFileTypes, allowFileTypes);
			AttachmentPolicy.LoadDictionary(out this.mimeTypeLevels, blockMimeTypes, forceSaveMimeTypes, allowMimeTypes);
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x000709D5 File Offset: 0x0006EBD5
		public static int MaxEmbeddedDepth
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x000709D8 File Offset: 0x0006EBD8
		public bool AlwaysBlock
		{
			get
			{
				return this.alwaysBlock;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x000709E0 File Offset: 0x0006EBE0
		public bool BlockOnPublicComputers
		{
			get
			{
				return this.blockOnPublicComputers;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x000709E8 File Offset: 0x0006EBE8
		public AttachmentPolicy.Level TreatUnknownTypeAs
		{
			get
			{
				return this.treatUnknownTypeAs;
			}
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000709F0 File Offset: 0x0006EBF0
		public AttachmentPolicy.Level GetLevel(string attachmentType, AttachmentPolicy.TypeSignifier typeSignifier)
		{
			AirSyncDiagnostics.Assert(attachmentType != null);
			AttachmentPolicy.Level result = AttachmentPolicy.Level.Unknown;
			switch (typeSignifier)
			{
			case AttachmentPolicy.TypeSignifier.File:
				result = AttachmentPolicy.FindLevel(this.fileTypeLevels, attachmentType);
				break;
			case AttachmentPolicy.TypeSignifier.Mime:
				result = AttachmentPolicy.FindLevel(this.mimeTypeLevels, attachmentType);
				break;
			}
			return result;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00070A3C File Offset: 0x0006EC3C
		private static AttachmentPolicy.Level FindLevel(SortedDictionary<string, AttachmentPolicy.Level> dictionary, string attachmentType)
		{
			AttachmentPolicy.Level result;
			if (!dictionary.TryGetValue(attachmentType, out result))
			{
				return AttachmentPolicy.Level.Unknown;
			}
			return result;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00070A58 File Offset: 0x0006EC58
		private static void LoadDictionary(out SortedDictionary<string, AttachmentPolicy.Level> dictionary, string[] block, string[] forceSave, string[] allow)
		{
			string[][] array = new string[3][];
			AttachmentPolicy.Level[] array2 = new AttachmentPolicy.Level[3];
			array[1] = block;
			array[2] = forceSave;
			array[0] = allow;
			array2[1] = AttachmentPolicy.Level.Block;
			array2[2] = AttachmentPolicy.Level.ForceSave;
			array2[0] = AttachmentPolicy.Level.Allow;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					array[i] = new string[0];
				}
			}
			dictionary = new SortedDictionary<string, AttachmentPolicy.Level>(StringComparer.OrdinalIgnoreCase);
			for (int j = 0; j < 2; j++)
			{
				for (int k = 0; k < array[j].Length; k++)
				{
					if (!dictionary.ContainsKey(array[j][k]))
					{
						dictionary.Add(array[j][k], array2[j]);
					}
				}
			}
		}

		// Token: 0x04000C0E RID: 3086
		private const int MaxEmbeddedDepthConstant = 4;

		// Token: 0x04000C0F RID: 3087
		private bool alwaysBlock;

		// Token: 0x04000C10 RID: 3088
		private bool blockOnPublicComputers;

		// Token: 0x04000C11 RID: 3089
		private SortedDictionary<string, AttachmentPolicy.Level> fileTypeLevels;

		// Token: 0x04000C12 RID: 3090
		private SortedDictionary<string, AttachmentPolicy.Level> mimeTypeLevels;

		// Token: 0x04000C13 RID: 3091
		private AttachmentPolicy.Level treatUnknownTypeAs;

		// Token: 0x020001EE RID: 494
		public enum Level
		{
			// Token: 0x04000C15 RID: 3093
			Block = 1,
			// Token: 0x04000C16 RID: 3094
			ForceSave,
			// Token: 0x04000C17 RID: 3095
			Allow,
			// Token: 0x04000C18 RID: 3096
			Unknown
		}

		// Token: 0x020001EF RID: 495
		public enum TypeSignifier
		{
			// Token: 0x04000C1A RID: 3098
			File,
			// Token: 0x04000C1B RID: 3099
			Mime
		}

		// Token: 0x020001F0 RID: 496
		private enum LevelPrecedence
		{
			// Token: 0x04000C1D RID: 3101
			First,
			// Token: 0x04000C1E RID: 3102
			Allow = 0,
			// Token: 0x04000C1F RID: 3103
			Block,
			// Token: 0x04000C20 RID: 3104
			ForceSave,
			// Token: 0x04000C21 RID: 3105
			Last = 2
		}
	}
}
