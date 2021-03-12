using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MessageSecurity.MessageClassifications
{
	// Token: 0x0200000A RID: 10
	internal class ClassificationSummary
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000027E2 File Offset: 0x000009E2
		private ClassificationSummary()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000280C File Offset: 0x00000A0C
		public ClassificationSummary(string name, Guid classificationID, string locale, string displayName, string senderDescription, string recipientDescription, ClassificationDisplayPrecedenceLevel displayPrecedence, bool permissionMenuVisible, bool retainClassificationEnabled)
		{
			this.name = name;
			this.classificationID = classificationID;
			this.locale = locale;
			this.displayName = displayName;
			this.senderDescription = senderDescription;
			this.recipientDescription = recipientDescription;
			this.displayPrecedence = displayPrecedence;
			this.permissionMenuVisible = permissionMenuVisible;
			this.retainClassificationEnabled = retainClassificationEnabled;
			this.isClassified = true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000288C File Offset: 0x00000A8C
		public ClassificationSummary(ClassificationSummary classificationSummary)
		{
			this.name = classificationSummary.Name;
			this.classificationID = classificationSummary.ClassificationID;
			this.locale = classificationSummary.Locale;
			this.displayName = classificationSummary.DisplayName;
			this.senderDescription = classificationSummary.SenderDescription;
			this.recipientDescription = classificationSummary.RecipientDescription;
			this.displayPrecedence = classificationSummary.DisplayPrecedence;
			this.permissionMenuVisible = classificationSummary.PermissionMenuVisible;
			this.retainClassificationEnabled = classificationSummary.RetainClassificationEnabled;
			this.isClassified = true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002934 File Offset: 0x00000B34
		public ClassificationSummary(MessageClassification messageClassification)
		{
			this.name = messageClassification.Name;
			this.classificationID = messageClassification.ClassificationID;
			this.locale = messageClassification.Locale;
			this.displayName = messageClassification.DisplayName;
			this.senderDescription = messageClassification.SenderDescription;
			this.recipientDescription = messageClassification.RecipientDescription;
			this.displayPrecedence = messageClassification.DisplayPrecedence;
			this.permissionMenuVisible = messageClassification.PermissionMenuVisible;
			this.retainClassificationEnabled = messageClassification.RetainClassificationEnabled;
			this.isClassified = true;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000029DB File Offset: 0x00000BDB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000029E3 File Offset: 0x00000BE3
		public Guid ClassificationID
		{
			get
			{
				return this.classificationID;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000029EB File Offset: 0x00000BEB
		public string Locale
		{
			get
			{
				return this.locale;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000029F3 File Offset: 0x00000BF3
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000029FB File Offset: 0x00000BFB
		public string SenderDescription
		{
			get
			{
				return this.senderDescription;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002A03 File Offset: 0x00000C03
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002A0B File Offset: 0x00000C0B
		public string RecipientDescription
		{
			get
			{
				return this.recipientDescription;
			}
			set
			{
				this.recipientDescription = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002A14 File Offset: 0x00000C14
		public ClassificationDisplayPrecedenceLevel DisplayPrecedence
		{
			get
			{
				return this.displayPrecedence;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002A1C File Offset: 0x00000C1C
		public bool PermissionMenuVisible
		{
			get
			{
				return this.permissionMenuVisible;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002A24 File Offset: 0x00000C24
		public bool RetainClassificationEnabled
		{
			get
			{
				return this.retainClassificationEnabled;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002A2C File Offset: 0x00000C2C
		public bool IsClassified
		{
			get
			{
				return this.isClassified;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A34 File Offset: 0x00000C34
		public bool IsValid
		{
			get
			{
				return this != ClassificationSummary.Invalid;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002A44 File Offset: 0x00000C44
		public int Size
		{
			get
			{
				return ClassificationSummary.GetSize(this.name) + Marshal.SizeOf(typeof(Guid)) + ClassificationSummary.GetSize(this.locale) + ClassificationSummary.GetSize(this.displayName) + ClassificationSummary.GetSize(this.senderDescription) + ClassificationSummary.GetSize(this.recipientDescription) + 4 + 1 + 1 + 1;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002AA4 File Offset: 0x00000CA4
		private static int GetSize(string str)
		{
			return (string.IsNullOrEmpty(str) ? 0 : str.Length) * 2;
		}

		// Token: 0x0400002A RID: 42
		public static readonly ClassificationSummary Empty = new ClassificationSummary();

		// Token: 0x0400002B RID: 43
		public static readonly ClassificationSummary Invalid = new ClassificationSummary();

		// Token: 0x0400002C RID: 44
		private string name;

		// Token: 0x0400002D RID: 45
		private Guid classificationID;

		// Token: 0x0400002E RID: 46
		private string locale;

		// Token: 0x0400002F RID: 47
		private string displayName = string.Empty;

		// Token: 0x04000030 RID: 48
		private string senderDescription = string.Empty;

		// Token: 0x04000031 RID: 49
		private string recipientDescription = string.Empty;

		// Token: 0x04000032 RID: 50
		private ClassificationDisplayPrecedenceLevel displayPrecedence;

		// Token: 0x04000033 RID: 51
		private bool permissionMenuVisible;

		// Token: 0x04000034 RID: 52
		private bool retainClassificationEnabled;

		// Token: 0x04000035 RID: 53
		private bool isClassified;
	}
}
