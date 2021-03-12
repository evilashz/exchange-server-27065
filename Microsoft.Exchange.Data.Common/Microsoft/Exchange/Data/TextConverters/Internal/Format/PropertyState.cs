using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002C0 RID: 704
	internal class PropertyState
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x000D6562 File Offset: 0x000D4762
		public int UndoStackTop
		{
			get
			{
				return this.propertyUndoStackTop;
			}
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x000D656A File Offset: 0x000D476A
		public FlagProperties GetEffectiveFlags()
		{
			return this.flagProperties;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000D6572 File Offset: 0x000D4772
		public FlagProperties GetDistinctFlags()
		{
			return this.distinctFlagProperties;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000D657A File Offset: 0x000D477A
		public PropertyValue GetEffectiveProperty(PropertyId id)
		{
			if (FlagProperties.IsFlagProperty(id))
			{
				return this.flagProperties.GetPropertyValue(id);
			}
			if (this.propertyMask.IsSet(id))
			{
				return this.properties[(int)(id - PropertyId.FontColor)];
			}
			return PropertyValue.Null;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000D65BA File Offset: 0x000D47BA
		public PropertyValue GetDistinctProperty(PropertyId id)
		{
			if (FlagProperties.IsFlagProperty(id))
			{
				return this.distinctFlagProperties.GetPropertyValue(id);
			}
			if (this.distinctPropertyMask.IsSet(id))
			{
				return this.properties[(int)(id - PropertyId.FontColor)];
			}
			return PropertyValue.Null;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000D65FC File Offset: 0x000D47FC
		public void SubtractDefaultFromDistinct(FlagProperties defaultFlags, Property[] defaultProperties)
		{
			FlagProperties y = defaultFlags ^ this.distinctFlagProperties;
			FlagProperties y2 = (this.distinctFlagProperties & y) | (this.distinctFlagProperties & ~defaultFlags);
			if (this.distinctFlagProperties != y2)
			{
				this.PushUndoEntry((PropertyId)74, this.distinctFlagProperties);
				this.distinctFlagProperties = y2;
			}
			if (defaultProperties != null)
			{
				bool flag = false;
				foreach (Property property in defaultProperties)
				{
					if (this.distinctPropertyMask.IsSet(property.Id) && this.properties[(int)(property.Id - PropertyId.FontColor)] == property.Value)
					{
						if (!flag)
						{
							this.PushUndoEntry(this.distinctPropertyMask);
							flag = true;
						}
						this.distinctPropertyMask.Clear(property.Id);
					}
				}
			}
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000D66EC File Offset: 0x000D48EC
		public int ApplyProperties(FlagProperties flagProperties, Property[] propList, FlagProperties flagInheritanceMask, PropertyBitMask propertyInheritanceMask)
		{
			int result = this.propertyUndoStackTop;
			FlagProperties x = this.flagProperties & flagInheritanceMask;
			FlagProperties x2 = x | flagProperties;
			if (x2 != this.flagProperties)
			{
				this.PushUndoEntry(PropertyId.MaxValue, this.flagProperties);
				this.flagProperties = x2;
			}
			FlagProperties y = x ^ flagProperties;
			FlagProperties x3 = (flagProperties & y) | (flagProperties & ~x);
			if (x3 != this.distinctFlagProperties)
			{
				this.PushUndoEntry((PropertyId)74, this.distinctFlagProperties);
				this.distinctFlagProperties = x3;
			}
			PropertyBitMask propertyBitMask = this.propertyMask & ~propertyInheritanceMask;
			foreach (PropertyId propertyId in propertyBitMask)
			{
				this.PushUndoEntry(propertyId, this.properties[(int)(propertyId - PropertyId.FontColor)]);
			}
			PropertyBitMask allOff = PropertyBitMask.AllOff;
			this.propertyMask &= propertyInheritanceMask;
			if (propList != null)
			{
				foreach (Property property in propList)
				{
					if (this.propertyMask.IsSet(property.Id))
					{
						if (this.properties[(int)(property.Id - PropertyId.FontColor)] != property.Value)
						{
							this.PushUndoEntry(property.Id, this.properties[(int)(property.Id - PropertyId.FontColor)]);
							if (property.Value.IsNull)
							{
								this.propertyMask.Clear(property.Id);
							}
							else
							{
								this.properties[(int)(property.Id - PropertyId.FontColor)] = property.Value;
								allOff.Set(property.Id);
							}
						}
					}
					else if (!property.Value.IsNull)
					{
						if (!propertyBitMask.IsSet(property.Id))
						{
							this.PushUndoEntry(property.Id, PropertyValue.Null);
						}
						this.properties[(int)(property.Id - PropertyId.FontColor)] = property.Value;
						this.propertyMask.Set(property.Id);
						allOff.Set(property.Id);
					}
				}
			}
			if (allOff != this.distinctPropertyMask)
			{
				this.PushUndoEntry(this.distinctPropertyMask);
				this.distinctPropertyMask = allOff;
			}
			return result;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000D697C File Offset: 0x000D4B7C
		public void UndoProperties(int undoLevel)
		{
			for (int i = this.propertyUndoStackTop - 1; i >= undoLevel; i--)
			{
				if (this.propertyUndoStack[i].IsFlags)
				{
					this.flagProperties = this.propertyUndoStack[i].Flags.Flags;
				}
				else if (this.propertyUndoStack[i].IsDistinctFlags)
				{
					this.distinctFlagProperties = this.propertyUndoStack[i].Flags.Flags;
				}
				else if (this.propertyUndoStack[i].IsDistinctMask1)
				{
					this.distinctPropertyMask.Set1(this.propertyUndoStack[i].Bits.Bits);
				}
				else if (this.propertyUndoStack[i].IsDistinctMask2)
				{
					this.distinctPropertyMask.Set2(this.propertyUndoStack[i].Bits.Bits);
				}
				else if (this.propertyUndoStack[i].Property.Value.IsNull)
				{
					this.propertyMask.Clear(this.propertyUndoStack[i].Property.Id);
				}
				else
				{
					this.properties[(int)(this.propertyUndoStack[i].Property.Id - PropertyId.FontColor)] = this.propertyUndoStack[i].Property.Value;
					this.propertyMask.Set(this.propertyUndoStack[i].Property.Id);
				}
			}
			this.propertyUndoStackTop = undoLevel;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000D6B28 File Offset: 0x000D4D28
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"flags: (",
				this.flagProperties.ToString(),
				"), props: (",
				this.propertyMask.ToString(),
				"), dflags: (",
				this.distinctFlagProperties.ToString(),
				"), dprops: (",
				this.distinctPropertyMask.ToString(),
				")"
			});
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000D6BBC File Offset: 0x000D4DBC
		private void PushUndoEntry(PropertyId id, PropertyValue value)
		{
			if (this.propertyUndoStackTop == this.propertyUndoStack.Length)
			{
				if (this.propertyUndoStack.Length >= 8960)
				{
					throw new TextConvertersException("property undo stack is too large");
				}
				int num = Math.Min(this.propertyUndoStack.Length * 2, 8960);
				PropertyState.PropertyUndoEntry[] destinationArray = new PropertyState.PropertyUndoEntry[num];
				Array.Copy(this.propertyUndoStack, 0, destinationArray, 0, this.propertyUndoStackTop);
				this.propertyUndoStack = destinationArray;
			}
			this.propertyUndoStack[this.propertyUndoStackTop++].Set(id, value);
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000D6C50 File Offset: 0x000D4E50
		private void PushUndoEntry(PropertyId fakePropId, FlagProperties flagProperties)
		{
			if (this.propertyUndoStackTop == this.propertyUndoStack.Length)
			{
				if (this.propertyUndoStack.Length >= 8960)
				{
					throw new TextConvertersException("property undo stack is too large");
				}
				int num = Math.Min(this.propertyUndoStack.Length * 2, 8960);
				PropertyState.PropertyUndoEntry[] destinationArray = new PropertyState.PropertyUndoEntry[num];
				Array.Copy(this.propertyUndoStack, 0, destinationArray, 0, this.propertyUndoStackTop);
				this.propertyUndoStack = destinationArray;
			}
			this.propertyUndoStack[this.propertyUndoStackTop++].Set(fakePropId, flagProperties);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000D6CE4 File Offset: 0x000D4EE4
		private void PushUndoEntry(PropertyBitMask propertyMask)
		{
			if (this.propertyUndoStackTop + 1 >= this.propertyUndoStack.Length)
			{
				if (this.propertyUndoStackTop + 2 >= 8960)
				{
					throw new TextConvertersException("property undo stack is too large");
				}
				int num = Math.Min(this.propertyUndoStack.Length * 2, 8960);
				PropertyState.PropertyUndoEntry[] destinationArray = new PropertyState.PropertyUndoEntry[num];
				Array.Copy(this.propertyUndoStack, 0, destinationArray, 0, this.propertyUndoStackTop);
				this.propertyUndoStack = destinationArray;
			}
			this.propertyUndoStack[this.propertyUndoStackTop++].Set((PropertyId)75, propertyMask.Bits1);
			this.propertyUndoStack[this.propertyUndoStackTop++].Set((PropertyId)76, propertyMask.Bits2);
		}

		// Token: 0x04002141 RID: 8513
		private const int MaxStackSize = 8960;

		// Token: 0x04002142 RID: 8514
		private FlagProperties flagProperties;

		// Token: 0x04002143 RID: 8515
		private FlagProperties distinctFlagProperties;

		// Token: 0x04002144 RID: 8516
		private PropertyBitMask propertyMask;

		// Token: 0x04002145 RID: 8517
		private PropertyBitMask distinctPropertyMask;

		// Token: 0x04002146 RID: 8518
		private PropertyValue[] properties = new PropertyValue[56];

		// Token: 0x04002147 RID: 8519
		private PropertyState.PropertyUndoEntry[] propertyUndoStack = new PropertyState.PropertyUndoEntry[146];

		// Token: 0x04002148 RID: 8520
		private int propertyUndoStackTop;

		// Token: 0x020002C1 RID: 705
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		private struct FlagPropertiesUndo
		{
			// Token: 0x04002149 RID: 8521
			public PropertyId FakeId;

			// Token: 0x0400214A RID: 8522
			public FlagProperties Flags;
		}

		// Token: 0x020002C2 RID: 706
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		private struct BitsUndo
		{
			// Token: 0x0400214B RID: 8523
			public PropertyId FakeId;

			// Token: 0x0400214C RID: 8524
			public uint Bits;
		}

		// Token: 0x020002C3 RID: 707
		[StructLayout(LayoutKind.Explicit, Pack = 2)]
		private struct PropertyUndoEntry
		{
			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000D6DCD File Offset: 0x000D4FCD
			public bool IsFlags
			{
				get
				{
					return this.Property.Id == PropertyId.MaxValue;
				}
			}

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000D6DDE File Offset: 0x000D4FDE
			public bool IsDistinctFlags
			{
				get
				{
					return this.Property.Id == (PropertyId)74;
				}
			}

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000D6DEF File Offset: 0x000D4FEF
			public bool IsDistinctMask1
			{
				get
				{
					return this.Property.Id == (PropertyId)75;
				}
			}

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x06001C22 RID: 7202 RVA: 0x000D6E00 File Offset: 0x000D5000
			public bool IsDistinctMask2
			{
				get
				{
					return this.Property.Id == (PropertyId)76;
				}
			}

			// Token: 0x06001C23 RID: 7203 RVA: 0x000D6E11 File Offset: 0x000D5011
			public void Set(PropertyId id, PropertyValue value)
			{
				this.Property.Set(id, value);
			}

			// Token: 0x06001C24 RID: 7204 RVA: 0x000D6E20 File Offset: 0x000D5020
			public void Set(PropertyId fakePropId, FlagProperties flagProperties)
			{
				this.Flags.FakeId = fakePropId;
				this.Flags.Flags = flagProperties;
			}

			// Token: 0x06001C25 RID: 7205 RVA: 0x000D6E3A File Offset: 0x000D503A
			public void Set(PropertyId fakePropId, uint bits)
			{
				this.Bits.FakeId = fakePropId;
				this.Bits.Bits = bits;
			}

			// Token: 0x0400214D RID: 8525
			public const PropertyId FlagPropertiesFakeId = PropertyId.MaxValue;

			// Token: 0x0400214E RID: 8526
			public const PropertyId DistinctFlagPropertiesFakeId = (PropertyId)74;

			// Token: 0x0400214F RID: 8527
			public const PropertyId DistinctMask1FakeId = (PropertyId)75;

			// Token: 0x04002150 RID: 8528
			public const PropertyId DistinctMask2FakeId = (PropertyId)76;

			// Token: 0x04002151 RID: 8529
			[FieldOffset(0)]
			public Property Property;

			// Token: 0x04002152 RID: 8530
			[FieldOffset(0)]
			public PropertyState.FlagPropertiesUndo Flags;

			// Token: 0x04002153 RID: 8531
			[FieldOffset(0)]
			public PropertyState.BitsUndo Bits;
		}
	}
}
