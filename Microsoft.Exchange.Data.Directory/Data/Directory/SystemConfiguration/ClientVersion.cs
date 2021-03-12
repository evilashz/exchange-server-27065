using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000535 RID: 1333
	[XmlType(TypeName = "ClientVersion")]
	[Serializable]
	public sealed class ClientVersion : XMLSerializableBase
	{
		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000E23C3 File Offset: 0x000E05C3
		// (set) Token: 0x06003B49 RID: 15177 RVA: 0x000E23CB File Offset: 0x000E05CB
		[XmlIgnore]
		public Version Version { get; set; }

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06003B4A RID: 15178 RVA: 0x000E23D4 File Offset: 0x000E05D4
		// (set) Token: 0x06003B4B RID: 15179 RVA: 0x000E23E4 File Offset: 0x000E05E4
		[XmlAttribute("Version")]
		public string VersionString
		{
			get
			{
				return this.Version.ToString();
			}
			set
			{
				Version version;
				if (Version.TryParse(value, out version))
				{
					this.Version = version;
				}
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x000E2402 File Offset: 0x000E0602
		// (set) Token: 0x06003B4D RID: 15181 RVA: 0x000E240A File Offset: 0x000E060A
		[XmlAttribute("ExpirationDate")]
		public DateTime ExpirationDate { get; set; }

		// Token: 0x06003B4E RID: 15182 RVA: 0x000E2414 File Offset: 0x000E0614
		public static ClientVersion Parse(string clientVersionString)
		{
			if (string.IsNullOrEmpty(clientVersionString))
			{
				throw new ArgumentNullException("clientVersionString");
			}
			string[] array = clientVersionString.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 2)
			{
				throw new ArgumentException("clientVersionString");
			}
			ClientVersion result;
			try
			{
				Version version = Version.Parse(array[0]);
				DateTime dateTime = DateTime.Parse(array[1]);
				ClientVersion clientVersion = new ClientVersion
				{
					Version = version,
					ExpirationDate = dateTime.Date
				};
				result = clientVersion;
			}
			catch (FormatException ex)
			{
				throw new ArgumentException(string.Format("Unable to parse string {0} into a valid ClientVersion. {1}", clientVersionString, ex), ex);
			}
			return result;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000E24C0 File Offset: 0x000E06C0
		public override string ToString()
		{
			return string.Format("{0},{1}", this.Version, this.ExpirationDate);
		}
	}
}
