using System;
using System.IO;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public class CompressionConfig
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00028E50 File Offset: 0x00027050
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00028E58 File Offset: 0x00027058
		public CompressionConfig.CompressionProvider Provider { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00028E61 File Offset: 0x00027061
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00028E69 File Offset: 0x00027069
		public CoconetConfig CoconetConfig { get; set; }

		// Token: 0x06000899 RID: 2201 RVA: 0x00028E72 File Offset: 0x00027072
		internal CompressionConfig()
		{
			this.Provider = CompressionConfig.CompressionProvider.None;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00028E84 File Offset: 0x00027084
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

		// Token: 0x0600089B RID: 2203 RVA: 0x00028EF8 File Offset: 0x000270F8
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

		// Token: 0x020000D7 RID: 215
		public enum CompressionProvider
		{
			// Token: 0x040003B3 RID: 947
			None,
			// Token: 0x040003B4 RID: 948
			Xpress,
			// Token: 0x040003B5 RID: 949
			Coconet
		}
	}
}
