using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.PST;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200006B RID: 107
	internal class FastTransferStreamExtractor
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x0001B120 File Offset: 0x00019320
		public FastTransferStreamExtractor(ExtractContext context, bool removeMetadata)
		{
			this.extractContext = context;
			this.data = context.Item.Data;
			this.currentStreamBufferEnd = 0;
			this.removeMetadata = removeMetadata;
			this.fixedSizePropertyExtractor = new FastTransferStreamExtractor.FixedSizePropertyExtractor();
			this.variableSizePropertyExtractor = new FastTransferStreamExtractor.VariableSizePropertyExtractor();
			this.multivaluedFixedSizePropertyExtractor = new FastTransferStreamExtractor.MultivaluedFixedSizePropertyExtractor();
			this.multivaluedVariableSizePropertyExtractor = new FastTransferStreamExtractor.MultivaluedVariableSizePropertyExtractor();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001B1D8 File Offset: 0x000193D8
		public void Extract()
		{
			using (this.dataStream = new MemoryStream(this.data, false))
			{
				this.ReadStreamHeader();
				while (this.dataStream.Position < this.dataStream.Length)
				{
					Guid a;
					uint? num;
					string a2;
					PropertyTag propTag = this.ReadPropertyTag(out a, out num, out a2);
					ushort id = propTag.Id;
					switch (id)
					{
					case 16384:
						this.extractContext.EnterAttachmentContext();
						break;
					case 16385:
						this.extractContext.EnterMessageContext(null);
						break;
					case 16386:
						this.extractContext.ExitMessageContext();
						break;
					case 16387:
						this.extractContext.EnterRecipientContext();
						break;
					case 16388:
						this.extractContext.ExitRecipientContext();
						break;
					default:
						if (id != 16398)
						{
							if (id != 16406)
							{
								FastTransferStreamExtractor.PropertyExtractor propertyExtractor = this.GetPropertyExtractor(propTag);
								if (this.removeMetadata)
								{
									Func<object, object> propertyValueProcessor = null;
									if (propTag.Value == PropertyTag.Importance.Value)
									{
										propertyValueProcessor = delegate(object x)
										{
											((byte[])x)[0] = 1;
											return x;
										};
									}
									else if (propTag.Value == PropertyTag.MessageFlags.Value)
									{
										propertyValueProcessor = delegate(object x)
										{
											byte[] array = (byte[])x;
											byte[] array2 = array;
											int num2 = 0;
											array2[num2] |= 1;
											byte[] array3 = array;
											int num3 = 1;
											array3[num3] |= 4;
											return array;
										};
									}
									else if (propTag.IsNamedProperty && a == FastTransferStreamExtractor.PublicStringsPropertySet && a2 == "Keywords")
									{
										propertyValueProcessor = ((object x) => null);
									}
									propertyExtractor.PropertyValueProcessor = propertyValueProcessor;
								}
								propertyExtractor.Extract();
							}
							else
							{
								this.ReadBytes(4);
							}
						}
						else
						{
							this.extractContext.ExitAttachmentContext();
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001B3E0 File Offset: 0x000195E0
		private byte[] ReadBytes(int size)
		{
			if ((long)size + this.dataStream.Position > this.dataStream.Length)
			{
				throw new ExportException(ExportErrorType.MessageDataCorrupted, "Request reading more data than the message stream.");
			}
			byte[] array = new byte[size];
			int i = size;
			while (i > 0)
			{
				if (this.dataStream.Position + (long)i <= (long)this.currentStreamBufferEnd)
				{
					this.SafeReadBytes(array, size - i, i);
					i = 0;
				}
				else
				{
					int num = this.currentStreamBufferEnd - (int)this.dataStream.Position;
					this.SafeReadBytes(array, size - i, num);
					i -= num;
					byte[] array2 = new byte[4];
					this.SafeReadBytes(array2, 0, 4);
					if (BitConverter.ToUInt32(array2, 0) != 2U)
					{
						throw new ExportException(ExportErrorType.MessageDataCorrupted, string.Format(CultureInfo.CurrentCulture, "Unexpected data 0x{0}, expecting 0x00000002", new object[]
						{
							BitConverter.ToUInt32(array2, 0).ToString("X", CultureInfo.InvariantCulture)
						}));
					}
					this.SafeReadBytes(array2, 0, 4);
					int num2 = BitConverter.ToInt32(array2, 0);
					this.currentStreamBufferEnd += 8 + num2;
					this.CheckStreamBufferEnd();
				}
			}
			return array;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001B4F8 File Offset: 0x000196F8
		private PropertyTag ReadPropertyTag(out Guid propertySet, out uint? numericId, out string propertyName)
		{
			byte[] value = this.ReadBytes(4);
			PropertyTag result = default(PropertyTag);
			result.Value = BitConverter.ToUInt32(value, 0);
			if (result.IsNamedProperty)
			{
				byte[] b = this.ReadBytes(16);
				byte[] array = this.ReadBytes(1);
				propertySet = new Guid(b);
				uint num;
				if (array[0] == 0)
				{
					byte[] value2 = this.ReadBytes(4);
					numericId = new uint?(BitConverter.ToUInt32(value2, 0));
					num = (uint)this.extractContext.PstSession.ReadIdFromNamedProp(null, numericId.Value, propertySet, true);
					propertyName = null;
				}
				else
				{
					string @string;
					using (MemoryStream memoryStream = new MemoryStream())
					{
						byte[] array2;
						do
						{
							array2 = this.ReadBytes(2);
							memoryStream.Write(array2, 0, 2);
						}
						while (array2[0] != 0 || array2[1] != 0);
						memoryStream.Seek(0L, SeekOrigin.Begin);
						byte[] array3 = new byte[memoryStream.Length - 2L];
						memoryStream.Read(array3, 0, array3.Length);
						@string = Encoding.Unicode.GetString(array3);
					}
					num = (uint)this.extractContext.PstSession.ReadIdFromNamedProp(@string, 0U, propertySet, true);
					propertyName = @string;
					numericId = null;
				}
				result.Value = ((uint)result.Type | num << 16);
			}
			else
			{
				propertySet = Guid.Empty;
				numericId = null;
				propertyName = null;
			}
			return result;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001B668 File Offset: 0x00019868
		private FastTransferStreamExtractor.PropertyExtractor GetPropertyExtractor(PropertyTag propTag)
		{
			FastTransferStreamExtractor.PropertyExtractor propertyExtractor;
			if (propTag.IsMultivalued)
			{
				if (propTag.IsFixedSize)
				{
					propertyExtractor = this.multivaluedFixedSizePropertyExtractor;
				}
				else
				{
					propertyExtractor = this.multivaluedVariableSizePropertyExtractor;
				}
			}
			else if (propTag.IsFixedSize)
			{
				propertyExtractor = this.fixedSizePropertyExtractor;
			}
			else
			{
				propertyExtractor = this.variableSizePropertyExtractor;
			}
			propertyExtractor.PropertyTag = propTag;
			propertyExtractor.PropertyBag = this.extractContext.CurrentPropertyBag;
			propertyExtractor.StreamExtractor = this;
			propertyExtractor.PropertyValueProcessor = null;
			return propertyExtractor;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001B6DC File Offset: 0x000198DC
		private void SafeReadBytes(byte[] buffer, int offset, int size)
		{
			int num = this.dataStream.Read(buffer, offset, size);
			if (num != size)
			{
				throw new ExportException(ExportErrorType.MessageDataCorrupted, string.Format(CultureInfo.CurrentCulture, "SafeReadBytes: requesting {0} bytes but only {1} bytes left", new object[]
				{
					size,
					num
				}));
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001B72C File Offset: 0x0001992C
		private void ReadStreamHeader()
		{
			byte[] array = new byte[4];
			byte[] array2 = new byte[4];
			int num2;
			for (;;)
			{
				this.SafeReadBytes(array, 0, 4);
				this.SafeReadBytes(array2, 0, 4);
				uint num = BitConverter.ToUInt32(array, 0);
				num2 = BitConverter.ToInt32(array2, 0);
				if (num == 2U)
				{
					break;
				}
				this.dataStream.Seek((long)num2, SeekOrigin.Current);
				if (this.dataStream.Position > this.dataStream.Length)
				{
					goto Block_2;
				}
			}
			this.currentStreamBufferEnd = (int)this.dataStream.Position + num2;
			this.CheckStreamBufferEnd();
			return;
			Block_2:
			throw new ExportException(ExportErrorType.MessageDataCorrupted, "Reading message stream header. The message stream end reached unexpectedly.");
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001B7BC File Offset: 0x000199BC
		private void CheckStreamBufferEnd()
		{
			if (this.currentStreamBufferEnd < 0 || this.currentStreamBufferEnd > this.data.Length)
			{
				throw new ExportException(ExportErrorType.MessageDataCorrupted, string.Format(CultureInfo.CurrentCulture, "The FX stream ops code 0x00000002 indicated wrong end position of the buffer. It is {0}. It should be less than the stream length {1}", new object[]
				{
					this.currentStreamBufferEnd,
					this.data.Length
				}));
			}
		}

		// Token: 0x04000288 RID: 648
		private const uint FastTransferBufferOpCode = 2U;

		// Token: 0x04000289 RID: 649
		private const string CategoriesPropertyName = "Keywords";

		// Token: 0x0400028A RID: 650
		private static readonly Guid PublicStringsPropertySet = new Guid("00020329-0000-0000-C000-000000000046");

		// Token: 0x0400028B RID: 651
		private readonly byte[] data;

		// Token: 0x0400028C RID: 652
		private readonly ExtractContext extractContext;

		// Token: 0x0400028D RID: 653
		private readonly bool removeMetadata;

		// Token: 0x0400028E RID: 654
		private readonly FastTransferStreamExtractor.FixedSizePropertyExtractor fixedSizePropertyExtractor;

		// Token: 0x0400028F RID: 655
		private readonly FastTransferStreamExtractor.VariableSizePropertyExtractor variableSizePropertyExtractor;

		// Token: 0x04000290 RID: 656
		private readonly FastTransferStreamExtractor.MultivaluedFixedSizePropertyExtractor multivaluedFixedSizePropertyExtractor;

		// Token: 0x04000291 RID: 657
		private readonly FastTransferStreamExtractor.MultivaluedVariableSizePropertyExtractor multivaluedVariableSizePropertyExtractor;

		// Token: 0x04000292 RID: 658
		private MemoryStream dataStream;

		// Token: 0x04000293 RID: 659
		private int currentStreamBufferEnd;

		// Token: 0x0200006C RID: 108
		private abstract class PropertyExtractor
		{
			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000793 RID: 1939 RVA: 0x0001B82F File Offset: 0x00019A2F
			// (set) Token: 0x06000794 RID: 1940 RVA: 0x0001B837 File Offset: 0x00019A37
			public FastTransferStreamExtractor StreamExtractor { get; set; }

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000795 RID: 1941 RVA: 0x0001B840 File Offset: 0x00019A40
			// (set) Token: 0x06000796 RID: 1942 RVA: 0x0001B848 File Offset: 0x00019A48
			public IPropertyBag PropertyBag { get; set; }

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x0001B851 File Offset: 0x00019A51
			// (set) Token: 0x06000798 RID: 1944 RVA: 0x0001B859 File Offset: 0x00019A59
			public PropertyTag PropertyTag { get; set; }

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001B862 File Offset: 0x00019A62
			// (set) Token: 0x0600079A RID: 1946 RVA: 0x0001B86A File Offset: 0x00019A6A
			public Func<object, object> PropertyValueProcessor { get; set; }

			// Token: 0x0600079B RID: 1947
			public abstract void Extract();

			// Token: 0x0600079C RID: 1948 RVA: 0x0001B874 File Offset: 0x00019A74
			protected static byte[] TrimStringEnd(PropertyTag.PropertyType propType, byte[] data)
			{
				int num = 0;
				switch (propType)
				{
				case PropertyTag.PropertyType.AnsiString:
					goto IL_43;
				case PropertyTag.PropertyType.String:
					break;
				default:
					if (propType != PropertyTag.PropertyType.Unicode)
					{
						if (propType != PropertyTag.PropertyType.Ascii)
						{
							goto IL_54;
						}
						goto IL_43;
					}
					break;
				}
				if (data.Length >= 2 && data[data.Length - 1] == 0 && data[data.Length - 2] == 0)
				{
					num = 2;
					goto IL_54;
				}
				goto IL_54;
				IL_43:
				if (data.Length >= 1 && data[data.Length - 1] == 0)
				{
					num = 1;
				}
				IL_54:
				if (num > 0)
				{
					byte[] array = new byte[data.Length - num];
					Array.Copy(data, array, array.Length);
					data = array;
				}
				return data;
			}
		}

		// Token: 0x0200006D RID: 109
		private class FixedSizePropertyExtractor : FastTransferStreamExtractor.PropertyExtractor
		{
			// Token: 0x0600079E RID: 1950 RVA: 0x0001B8FC File Offset: 0x00019AFC
			public override void Extract()
			{
				int sizeOfFixedSizeProperty = PropertyTag.GetSizeOfFixedSizeProperty(base.PropertyTag.Type);
				if (sizeOfFixedSizeProperty > 0)
				{
					byte[] array = base.StreamExtractor.ReadBytes(sizeOfFixedSizeProperty);
					if (base.PropertyTag.Type == PropertyTag.PropertyType.Boolean)
					{
						array = new byte[]
						{
							array[0]
						};
					}
					if (base.PropertyValueProcessor != null)
					{
						array = (byte[])base.PropertyValueProcessor(array);
					}
					if (array != null)
					{
						IProperty property = base.PropertyBag.AddProperty(base.PropertyTag.Value);
						IPropertyWriter propertyWriter = property.OpenStreamWriter();
						propertyWriter.Write(array);
						propertyWriter.Close();
					}
				}
			}
		}

		// Token: 0x0200006E RID: 110
		private class VariableSizePropertyExtractor : FastTransferStreamExtractor.PropertyExtractor
		{
			// Token: 0x060007A0 RID: 1952 RVA: 0x0001B9A8 File Offset: 0x00019BA8
			public override void Extract()
			{
				byte[] value = base.StreamExtractor.ReadBytes(4);
				int size = BitConverter.ToInt32(value, 0);
				byte[] array = base.StreamExtractor.ReadBytes(size);
				if (base.PropertyValueProcessor != null)
				{
					array = (byte[])base.PropertyValueProcessor(array);
				}
				if (array != null)
				{
					IProperty property = base.PropertyBag.AddProperty(base.PropertyTag.NormalizedValueForPst);
					IPropertyWriter propertyWriter = property.OpenStreamWriter();
					propertyWriter.Write(FastTransferStreamExtractor.PropertyExtractor.TrimStringEnd(base.PropertyTag.Type, array));
					propertyWriter.Close();
				}
			}
		}

		// Token: 0x0200006F RID: 111
		private class MultivaluedFixedSizePropertyExtractor : FastTransferStreamExtractor.PropertyExtractor
		{
			// Token: 0x060007A2 RID: 1954 RVA: 0x0001BA44 File Offset: 0x00019C44
			public override void Extract()
			{
				byte[] value = base.StreamExtractor.ReadBytes(4);
				int num = BitConverter.ToInt32(value, 0);
				List<List<byte>> list = new List<List<byte>>(num);
				int sizeOfFixedSizeProperty = PropertyTag.GetSizeOfFixedSizeProperty(base.PropertyTag.Type);
				for (int i = 0; i < num; i++)
				{
					byte[] collection = base.StreamExtractor.ReadBytes(sizeOfFixedSizeProperty);
					List<byte> item = new List<byte>(collection);
					list.Add(item);
				}
				if (base.PropertyValueProcessor != null)
				{
					list = (List<List<byte>>)base.PropertyValueProcessor(list);
				}
				if (list != null)
				{
					IProperty property = base.PropertyBag.AddProperty(base.PropertyTag.NormalizedValueForPst);
					property.WriteMultiValueData(list);
				}
			}
		}

		// Token: 0x02000070 RID: 112
		private class MultivaluedVariableSizePropertyExtractor : FastTransferStreamExtractor.PropertyExtractor
		{
			// Token: 0x060007A4 RID: 1956 RVA: 0x0001BAFC File Offset: 0x00019CFC
			public override void Extract()
			{
				byte[] value = base.StreamExtractor.ReadBytes(4);
				int num = BitConverter.ToInt32(value, 0);
				List<List<byte>> list = new List<List<byte>>(num);
				for (int i = 0; i < num; i++)
				{
					byte[] value2 = base.StreamExtractor.ReadBytes(4);
					int size = BitConverter.ToInt32(value2, 0);
					byte[] data = base.StreamExtractor.ReadBytes(size);
					List<byte> item = new List<byte>(FastTransferStreamExtractor.PropertyExtractor.TrimStringEnd(base.PropertyTag.Type & ~PropertyTag.PropertyType.Multivalued, data));
					list.Add(item);
				}
				if (base.PropertyValueProcessor != null)
				{
					list = (List<List<byte>>)base.PropertyValueProcessor(list);
				}
				if (list != null)
				{
					IProperty property = base.PropertyBag.AddProperty(base.PropertyTag.NormalizedValueForPst);
					property.WriteMultiValueData(list);
				}
			}
		}
	}
}
