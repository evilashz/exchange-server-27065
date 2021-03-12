using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005F1 RID: 1521
	[XmlInclude(typeof(CustomMenuKeyMapping))]
	[Serializable]
	public class AutoAttendantSettings
	{
		// Token: 0x06004802 RID: 18434 RVA: 0x0010968A File Offset: 0x0010788A
		public AutoAttendantSettings()
		{
			this.Parent = null;
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x00109699 File Offset: 0x00107899
		public AutoAttendantSettings(UMAutoAttendant p, bool businessHourSetting)
		{
			this.Parent = p;
			this.IsBusinessHourSetting = businessHourSetting;
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x001096AF File Offset: 0x001078AF
		public AutoAttendantSettings(UMAutoAttendant p) : this(p, false)
		{
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x001096BC File Offset: 0x001078BC
		public static AutoAttendantSettings FromXml(string xml)
		{
			object obj = null;
			try
			{
				obj = SerializationHelper.Deserialize(xml, typeof(AutoAttendantSettings));
			}
			catch
			{
			}
			return (AutoAttendantSettings)obj;
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x001096F8 File Offset: 0x001078F8
		public static string ToXml(AutoAttendantSettings aa)
		{
			return SerializationHelper.Serialize(aa);
		}

		// Token: 0x040031A0 RID: 12704
		public string TimeZoneKeyName;

		// Token: 0x040031A1 RID: 12705
		public bool IsBusinessHourSetting;

		// Token: 0x040031A2 RID: 12706
		public string WelcomeGreetingFilename;

		// Token: 0x040031A3 RID: 12707
		public bool WelcomeGreetingEnabled;

		// Token: 0x040031A4 RID: 12708
		public string GlobalInfoAnnouncementFilename;

		// Token: 0x040031A5 RID: 12709
		public bool MainMenuCustomPromptEnabled;

		// Token: 0x040031A6 RID: 12710
		public string MainMenuCustomPromptFilename;

		// Token: 0x040031A7 RID: 12711
		public bool TransferToOperatorEnabled;

		// Token: 0x040031A8 RID: 12712
		public string GlobalOperatorExtension;

		// Token: 0x040031A9 RID: 12713
		public bool KeyMappingEnabled;

		// Token: 0x040031AA RID: 12714
		public CustomMenuKeyMapping[] KeyMapping;

		// Token: 0x040031AB RID: 12715
		[XmlIgnore]
		public UMAutoAttendant Parent;
	}
}
