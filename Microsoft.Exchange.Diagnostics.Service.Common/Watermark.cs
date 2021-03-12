using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200001B RID: 27
	[DataContract(Name = "Watermark")]
	public sealed class Watermark
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00005820 File Offset: 0x00003A20
		public Watermark(DateTime timestamp)
		{
			this.timestamp = timestamp;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000582F File Offset: 0x00003A2F
		[IgnoreDataMember]
		public static Watermark LatestWatermark
		{
			get
			{
				return Watermark.latestWatermark;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00005836 File Offset: 0x00003A36
		[IgnoreDataMember]
		public DateTime Timestamp
		{
			get
			{
				return this.timestamp;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005840 File Offset: 0x00003A40
		public static Watermark Load(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				throw new ArgumentNullException("filename");
			}
			Watermark result = null;
			using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Watermark));
				result = (Watermark)dataContractSerializer.ReadObject(fileStream);
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000058A8 File Offset: 0x00003AA8
		public static IDictionary<string, Watermark> LoadWatermarksFromDirectory(string watermarksDirectory)
		{
			if (string.IsNullOrEmpty(watermarksDirectory))
			{
				throw new ArgumentNullException("watermarksDirectory");
			}
			string[] files = Directory.GetFiles(watermarksDirectory, string.Format("{0}_Watermark.xml", "*"));
			Dictionary<string, Watermark> dictionary = new Dictionary<string, Watermark>();
			foreach (string text in files)
			{
				string fileName = Path.GetFileName(text);
				int num = fileName.LastIndexOf(string.Format("{0}_Watermark.xml", string.Empty));
				if (num != -1)
				{
					string text2 = fileName.Substring(0, num);
					if (!string.IsNullOrEmpty(text2))
					{
						try
						{
							Watermark watermark = Watermark.Load(text);
							if (watermark == null)
							{
								watermark = Watermark.LatestWatermark;
							}
							dictionary[text2] = watermark;
						}
						catch (FileNotFoundException)
						{
							Logger.LogErrorMessage("Unable to open watermark file '{0}' which was expected to exist.", new object[]
							{
								text
							});
						}
						catch (SerializationException ex)
						{
							Logger.LogErrorMessage("Unable to de-serialize the watermark file '{0}'. Exception: {1}", new object[]
							{
								text,
								ex
							});
						}
						catch (XmlException ex2)
						{
							Logger.LogErrorMessage("Unable to parse XML of the watermark file '{0}'. Exception: {1}", new object[]
							{
								text,
								ex2
							});
						}
						catch (Exception ex3)
						{
							Logger.LogErrorMessage("An unexpected exception was caught while trying to read a watermark. Exception: {0}", new object[]
							{
								ex3
							});
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005A1C File Offset: 0x00003C1C
		public override bool Equals(object obj)
		{
			Watermark watermark = obj as Watermark;
			return watermark != null && this.timestamp.Equals(watermark.timestamp);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005A4C File Offset: 0x00003C4C
		public override int GetHashCode()
		{
			return this.timestamp.GetHashCode();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005A70 File Offset: 0x00003C70
		public void Save(FileStream fileStream)
		{
			try
			{
				if (fileStream == null)
				{
					throw new ArgumentNullException("fileStream");
				}
			}
			finally
			{
				fileStream.SetLength(0L);
				DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Watermark));
				dataContractSerializer.WriteObject(fileStream, this);
				fileStream.Flush();
			}
		}

		// Token: 0x040002EC RID: 748
		[IgnoreDataMember]
		public const string WatermarkFileNameFormat = "{0}_Watermark.xml";

		// Token: 0x040002ED RID: 749
		private static readonly Watermark latestWatermark = new Watermark(DateTime.UtcNow);

		// Token: 0x040002EE RID: 750
		[DataMember(Name = "Timestamp", IsRequired = true)]
		private readonly DateTime timestamp;
	}
}
