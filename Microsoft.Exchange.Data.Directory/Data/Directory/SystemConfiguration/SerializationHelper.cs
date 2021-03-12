using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005ED RID: 1517
	internal class SerializationHelper
	{
		// Token: 0x060047F9 RID: 18425 RVA: 0x001094E8 File Offset: 0x001076E8
		internal static string Serialize(object o)
		{
			AutoAttendantSettingsSerializer autoAttendantSettingsSerializer = new AutoAttendantSettingsSerializer();
			StringBuilder stringBuilder = new StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder);
			autoAttendantSettingsSerializer.Serialize(stringWriter, o);
			stringWriter.Flush();
			stringWriter.Close();
			return stringBuilder.ToString();
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x00109524 File Offset: 0x00107724
		internal static object Deserialize(string text, Type t)
		{
			AutoAttendantSettingsSerializer autoAttendantSettingsSerializer = new AutoAttendantSettingsSerializer();
			StringReader stringReader = new StringReader(text);
			object result = autoAttendantSettingsSerializer.Deserialize(stringReader);
			stringReader.Close();
			return result;
		}
	}
}
