using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.LocStrings
{
	// Token: 0x0200001E RID: 30
	internal static class Strings
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x0000641C File Offset: 0x0000461C
		static Strings()
		{
			Strings.stringIDs.Add(2339760653U, "TaskTypeUnknown");
			Strings.stringIDs.Add(952580535U, "TaskCannotBeRestored");
			Strings.stringIDs.Add(4014022951U, "TaskIsDisabled");
			Strings.stringIDs.Add(883701452U, "TaskNotFound");
			Strings.stringIDs.Add(386282707U, "TaskAlreadyExists");
			Strings.stringIDs.Add(648305924U, "TenantMustBeSpecified");
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000064CF File Offset: 0x000046CF
		public static string TaskTypeUnknown
		{
			get
			{
				return Strings.ResourceManager.GetString("TaskTypeUnknown");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000064E0 File Offset: 0x000046E0
		public static string TaskCannotBeRestored
		{
			get
			{
				return Strings.ResourceManager.GetString("TaskCannotBeRestored");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000064F1 File Offset: 0x000046F1
		public static string TaskIsDisabled
		{
			get
			{
				return Strings.ResourceManager.GetString("TaskIsDisabled");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00006502 File Offset: 0x00004702
		public static string TaskNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("TaskNotFound");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006513 File Offset: 0x00004713
		public static string TaskAlreadyExists
		{
			get
			{
				return Strings.ResourceManager.GetString("TaskAlreadyExists");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00006524 File Offset: 0x00004724
		public static string TenantMustBeSpecified
		{
			get
			{
				return Strings.ResourceManager.GetString("TenantMustBeSpecified");
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006535 File Offset: 0x00004735
		public static string ErrorDuringDarCall(string correlationId)
		{
			return string.Format(Strings.ResourceManager.GetString("ErrorDuringDarCall"), correlationId);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000654C File Offset: 0x0000474C
		public static string GetLocalizedString(Strings.IDs key)
		{
			return Strings.ResourceManager.GetString(Strings.stringIDs[(uint)key]);
		}

		// Token: 0x0400009D RID: 157
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(6);

		// Token: 0x0400009E RID: 158
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Office.CompliancePolicy.Exchange.Dar.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200001F RID: 31
		public enum IDs : uint
		{
			// Token: 0x040000A0 RID: 160
			TaskTypeUnknown = 2339760653U,
			// Token: 0x040000A1 RID: 161
			TaskCannotBeRestored = 952580535U,
			// Token: 0x040000A2 RID: 162
			TaskIsDisabled = 4014022951U,
			// Token: 0x040000A3 RID: 163
			TaskNotFound = 883701452U,
			// Token: 0x040000A4 RID: 164
			TaskAlreadyExists = 386282707U,
			// Token: 0x040000A5 RID: 165
			TenantMustBeSpecified = 648305924U
		}

		// Token: 0x02000020 RID: 32
		private enum ParamIDs
		{
			// Token: 0x040000A7 RID: 167
			ErrorDuringDarCall
		}
	}
}
