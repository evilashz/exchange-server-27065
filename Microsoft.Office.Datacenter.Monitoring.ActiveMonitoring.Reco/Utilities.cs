using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000010 RID: 16
	public static class Utilities
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003540 File Offset: 0x00001740
		public static void SerializeObjectToFile(object obj, string fileName)
		{
			string directoryName = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read, 8, FileOptions.WriteThrough))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileStream, Encoding.Unicode))
				{
					xmlSerializer.Serialize(xmlTextWriter, obj);
					xmlTextWriter.Close();
				}
				fileStream.Flush(true);
				fileStream.Close();
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000035DC File Offset: 0x000017DC
		public static T DeserializeObjectFromFile<T>(string fileName)
		{
			T result = default(T);
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				result = (T)((object)xmlSerializer.Deserialize(fileStream));
				fileStream.Close();
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000363C File Offset: 0x0000183C
		public static string NormalizeStringToValidFileOrRegistryKeyName(string input)
		{
			return Regex.Replace(input, "[<>|:*?\\/]", "-");
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000365B File Offset: 0x0000185B
		public static bool IsSequenceNullOrEmpty<T>(IEnumerable<T> sequence)
		{
			return sequence == null || !sequence.Any<T>();
		}
	}
}
