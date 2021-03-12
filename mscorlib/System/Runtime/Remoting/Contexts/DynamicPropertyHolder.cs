using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007E3 RID: 2019
	internal class DynamicPropertyHolder
	{
		// Token: 0x060057B2 RID: 22450 RVA: 0x00134400 File Offset: 0x00132600
		[SecurityCritical]
		internal virtual bool AddDynamicProperty(IDynamicProperty prop)
		{
			bool result;
			lock (this)
			{
				DynamicPropertyHolder.CheckPropertyNameClash(prop.Name, this._props, this._numProps);
				bool flag2 = false;
				if (this._props == null || this._numProps == this._props.Length)
				{
					this._props = DynamicPropertyHolder.GrowPropertiesArray(this._props);
					flag2 = true;
				}
				IDynamicProperty[] props = this._props;
				int numProps = this._numProps;
				this._numProps = numProps + 1;
				props[numProps] = prop;
				if (flag2)
				{
					this._sinks = DynamicPropertyHolder.GrowDynamicSinksArray(this._sinks);
				}
				if (this._sinks == null)
				{
					this._sinks = new IDynamicMessageSink[this._props.Length];
					for (int i = 0; i < this._numProps; i++)
					{
						this._sinks[i] = ((IContributeDynamicSink)this._props[i]).GetDynamicSink();
					}
				}
				else
				{
					this._sinks[this._numProps - 1] = ((IContributeDynamicSink)prop).GetDynamicSink();
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x00134514 File Offset: 0x00132714
		[SecurityCritical]
		internal virtual bool RemoveDynamicProperty(string name)
		{
			lock (this)
			{
				for (int i = 0; i < this._numProps; i++)
				{
					if (this._props[i].Name.Equals(name))
					{
						this._props[i] = this._props[this._numProps - 1];
						this._numProps--;
						this._sinks = null;
						return true;
					}
				}
				throw new RemotingException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), name));
			}
			bool result;
			return result;
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x060057B4 RID: 22452 RVA: 0x001345BC File Offset: 0x001327BC
		internal virtual IDynamicProperty[] DynamicProperties
		{
			get
			{
				if (this._props == null)
				{
					return null;
				}
				IDynamicProperty[] result;
				lock (this)
				{
					IDynamicProperty[] array = new IDynamicProperty[this._numProps];
					Array.Copy(this._props, array, this._numProps);
					result = array;
				}
				return result;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x060057B5 RID: 22453 RVA: 0x0013461C File Offset: 0x0013281C
		internal virtual ArrayWithSize DynamicSinks
		{
			[SecurityCritical]
			get
			{
				if (this._numProps == 0)
				{
					return null;
				}
				lock (this)
				{
					if (this._sinks == null)
					{
						this._sinks = new IDynamicMessageSink[this._numProps + 8];
						for (int i = 0; i < this._numProps; i++)
						{
							this._sinks[i] = ((IContributeDynamicSink)this._props[i]).GetDynamicSink();
						}
					}
				}
				return new ArrayWithSize(this._sinks, this._numProps);
			}
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x001346B4 File Offset: 0x001328B4
		private static IDynamicMessageSink[] GrowDynamicSinksArray(IDynamicMessageSink[] sinks)
		{
			int num = ((sinks != null) ? sinks.Length : 0) + 8;
			IDynamicMessageSink[] array = new IDynamicMessageSink[num];
			if (sinks != null)
			{
				Array.Copy(sinks, array, sinks.Length);
			}
			return array;
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x001346E4 File Offset: 0x001328E4
		[SecurityCritical]
		internal static void NotifyDynamicSinks(IMessage msg, ArrayWithSize dynSinks, bool bCliSide, bool bStart, bool bAsync)
		{
			for (int i = 0; i < dynSinks.Count; i++)
			{
				if (bStart)
				{
					dynSinks.Sinks[i].ProcessMessageStart(msg, bCliSide, bAsync);
				}
				else
				{
					dynSinks.Sinks[i].ProcessMessageFinish(msg, bCliSide, bAsync);
				}
			}
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x0013472C File Offset: 0x0013292C
		[SecurityCritical]
		internal static void CheckPropertyNameClash(string name, IDynamicProperty[] props, int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (props[i].Name.Equals(name))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DuplicatePropertyName"));
				}
			}
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x00134768 File Offset: 0x00132968
		internal static IDynamicProperty[] GrowPropertiesArray(IDynamicProperty[] props)
		{
			int num = ((props != null) ? props.Length : 0) + 8;
			IDynamicProperty[] array = new IDynamicProperty[num];
			if (props != null)
			{
				Array.Copy(props, array, props.Length);
			}
			return array;
		}

		// Token: 0x040027BA RID: 10170
		private const int GROW_BY = 8;

		// Token: 0x040027BB RID: 10171
		private IDynamicProperty[] _props;

		// Token: 0x040027BC RID: 10172
		private int _numProps;

		// Token: 0x040027BD RID: 10173
		private IDynamicMessageSink[] _sinks;
	}
}
