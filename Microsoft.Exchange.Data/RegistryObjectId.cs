using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000297 RID: 663
	[Serializable]
	public class RegistryObjectId : ObjectId
	{
		// Token: 0x06001801 RID: 6145 RVA: 0x0004B1AD File Offset: 0x000493AD
		public RegistryObjectId(string registryFolderPath) : this(registryFolderPath, null)
		{
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0004B1B7 File Offset: 0x000493B7
		public RegistryObjectId(string registryFolderPath, string registryFolderName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("registryFolderPath", registryFolderPath);
			this.RegistryKeyPath = registryFolderPath;
			this.Name = registryFolderName;
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0004B1D8 File Offset: 0x000493D8
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x0004B1E0 File Offset: 0x000493E0
		public string RegistryKeyPath { get; private set; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0004B1E9 File Offset: 0x000493E9
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x0004B1F1 File Offset: 0x000493F1
		public string Name { get; private set; }

		// Token: 0x06001807 RID: 6151 RVA: 0x0004B1FA File Offset: 0x000493FA
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = ((this.Name != null) ? string.Format("{0}\\{1}", this.RegistryKeyPath, this.Name) : this.RegistryKeyPath);
			}
			return this.toString;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0004B238 File Offset: 0x00049438
		public override bool Equals(object obj)
		{
			RegistryObjectId registryObjectId = obj as RegistryObjectId;
			return registryObjectId != null && (object.ReferenceEquals(this, registryObjectId) || (string.Equals(this.RegistryKeyPath, registryObjectId.RegistryKeyPath, StringComparison.OrdinalIgnoreCase) && string.Equals(this.Name, registryObjectId.Name, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0004B284 File Offset: 0x00049484
		public override int GetHashCode()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.RegistryKeyPath.GetHashCode() ^ this.Name.GetHashCode();
			}
			return this.RegistryKeyPath.GetHashCode();
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0004B2B8 File Offset: 0x000494B8
		public override byte[] GetBytes()
		{
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			Stream stream = new MemoryStream();
			binaryFormatter.Serialize(stream, this.RegistryKeyPath);
			binaryFormatter.Serialize(stream, this.Name);
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, (int)stream.Length);
			stream.Close();
			return array;
		}

		// Token: 0x04000E38 RID: 3640
		private string toString;
	}
}
