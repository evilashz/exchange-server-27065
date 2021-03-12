using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200072C RID: 1836
	[ComVisible(true)]
	public class SurrogateSelector : ISurrogateSelector
	{
		// Token: 0x060051CA RID: 20938 RVA: 0x0011EDB0 File Offset: 0x0011CFB0
		public SurrogateSelector()
		{
			this.m_surrogates = new SurrogateHashtable(32);
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x0011EDC8 File Offset: 0x0011CFC8
		public virtual void AddSurrogate(Type type, StreamingContext context, ISerializationSurrogate surrogate)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (surrogate == null)
			{
				throw new ArgumentNullException("surrogate");
			}
			SurrogateKey key = new SurrogateKey(type, context);
			this.m_surrogates.Add(key, surrogate);
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x0011EE0C File Offset: 0x0011D00C
		[SecurityCritical]
		private static bool HasCycle(ISurrogateSelector selector)
		{
			ISurrogateSelector surrogateSelector = selector;
			ISurrogateSelector surrogateSelector2 = selector;
			while (surrogateSelector != null)
			{
				surrogateSelector = surrogateSelector.GetNextSelector();
				if (surrogateSelector == null)
				{
					return true;
				}
				if (surrogateSelector == surrogateSelector2)
				{
					return false;
				}
				surrogateSelector = surrogateSelector.GetNextSelector();
				surrogateSelector2 = surrogateSelector2.GetNextSelector();
				if (surrogateSelector == surrogateSelector2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0011EE4C File Offset: 0x0011D04C
		[SecurityCritical]
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			if (selector == this)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_DuplicateSelector"));
			}
			if (!SurrogateSelector.HasCycle(selector))
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycleInArgument"), "selector");
			}
			ISurrogateSelector surrogateSelector = selector.GetNextSelector();
			ISurrogateSelector surrogateSelector2 = selector;
			while (surrogateSelector != null && surrogateSelector != this)
			{
				surrogateSelector2 = surrogateSelector;
				surrogateSelector = surrogateSelector.GetNextSelector();
			}
			if (surrogateSelector == this)
			{
				throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
			}
			surrogateSelector = selector;
			ISurrogateSelector surrogateSelector3 = selector;
			while (surrogateSelector != null)
			{
				if (surrogateSelector == surrogateSelector2)
				{
					surrogateSelector = this.GetNextSelector();
				}
				else
				{
					surrogateSelector = surrogateSelector.GetNextSelector();
				}
				if (surrogateSelector == null)
				{
					break;
				}
				if (surrogateSelector == surrogateSelector3)
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
				}
				if (surrogateSelector == surrogateSelector2)
				{
					surrogateSelector = this.GetNextSelector();
				}
				else
				{
					surrogateSelector = surrogateSelector.GetNextSelector();
				}
				if (surrogateSelector3 == surrogateSelector2)
				{
					surrogateSelector3 = this.GetNextSelector();
				}
				else
				{
					surrogateSelector3 = surrogateSelector3.GetNextSelector();
				}
				if (surrogateSelector == surrogateSelector3)
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_SurrogateCycle"), "selector");
				}
			}
			ISurrogateSelector nextSelector = this.m_nextSelector;
			this.m_nextSelector = selector;
			if (nextSelector != null)
			{
				surrogateSelector2.ChainSelector(nextSelector);
			}
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x0011EF5E File Offset: 0x0011D15E
		[SecurityCritical]
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this.m_nextSelector;
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0011EF68 File Offset: 0x0011D168
		[SecurityCritical]
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			selector = this;
			SurrogateKey key = new SurrogateKey(type, context);
			ISerializationSurrogate serializationSurrogate = (ISerializationSurrogate)this.m_surrogates[key];
			if (serializationSurrogate != null)
			{
				return serializationSurrogate;
			}
			if (this.m_nextSelector != null)
			{
				return this.m_nextSelector.GetSurrogate(type, context, out selector);
			}
			return null;
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0011EFC4 File Offset: 0x0011D1C4
		public virtual void RemoveSurrogate(Type type, StreamingContext context)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			SurrogateKey key = new SurrogateKey(type, context);
			this.m_surrogates.Remove(key);
		}

		// Token: 0x0400240A RID: 9226
		internal SurrogateHashtable m_surrogates;

		// Token: 0x0400240B RID: 9227
		internal ISurrogateSelector m_nextSelector;
	}
}
