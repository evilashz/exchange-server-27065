using System;
using System.IO;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rps
{
	// Token: 0x0200042D RID: 1069
	public class FailureCategoryList
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0009AFCF File Offset: 0x000991CF
		// (set) Token: 0x06001B8B RID: 7051 RVA: 0x0009AFD7 File Offset: 0x000991D7
		public FailureCategory[] FailureCategories { get; set; }

		// Token: 0x06001B8C RID: 7052 RVA: 0x0009AFE0 File Offset: 0x000991E0
		public static FailureCategoryList LoadFrom(string filePath)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(FailureCategoryList));
			TextReader textReader = new StreamReader(filePath);
			return (FailureCategoryList)xmlSerializer.Deserialize(textReader);
		}
	}
}
