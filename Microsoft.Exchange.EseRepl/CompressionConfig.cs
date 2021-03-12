using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class CompressionConfig
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000030A2 File Offset: 0x000012A2
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000030AA File Offset: 0x000012AA
		public CompressionConfig.CompressionProvider Provider { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000030B3 File Offset: 0x000012B3
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000030BB File Offset: 0x000012BB
		public CoconetConfig CoconetConfig { get; set; }

		// Token: 0x0600007C RID: 124 RVA: 0x000030C4 File Offset: 0x000012C4
		internal CompressionConfig()
		{
			this.Provider = CompressionConfig.CompressionProvider.None;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000030D4 File Offset: 0x000012D4
		internal static CompressionConfig Deserialize(string configXml, out Exception ex)
		{
			ex = null;
			try
			{
				if (!string.IsNullOrEmpty(configXml))
				{
					return (CompressionConfig)SerializationUtil.XmlToObject(configXml, typeof(CompressionConfig));
				}
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (SerializationException ex4)
			{
				ex = ex4;
			}
			return new CompressionConfig();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003148 File Offset: 0x00001348
		public override string ToString()
		{
			if (this.Provider == CompressionConfig.CompressionProvider.None)
			{
				return "None";
			}
			if (this.Provider == CompressionConfig.CompressionProvider.Xpress)
			{
				return "Xpress";
			}
			return string.Format("Coconet (DictionarySize={0}, SampleRate={1}, LzOption={2}", this.CoconetConfig.DictionarySize, this.CoconetConfig.SampleRate, this.CoconetConfig.LzOption);
		}

		// Token: 0x02000010 RID: 16
		public enum CompressionProvider
		{
			// Token: 0x0400003B RID: 59
			None,
			// Token: 0x0400003C RID: 60
			Xpress,
			// Token: 0x0400003D RID: 61
			Coconet
		}
	}
}
