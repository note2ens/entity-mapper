﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EntityMapper.Default;
using EntityMapper.Helper;

namespace EntityMapper.Default
{
    public class ManuelMap<TSource, UResult>
        where TSource : class
        where UResult : class
    {
        private readonly EntityMapper<TSource, UResult> mapper;
        public ManuelMap(EntityMapper<TSource, UResult> mapper)
        {
            this.mapper = mapper;

            if (mapper.resultInstance == null)
                mapper.resultInstance = Activator.CreateInstance(typeof(UResult));
        }

        public ManuelMap<TSource, UResult> ManualPropertyMap<TType>(Expression<Func<TSource, TType>> sourceProperty, Expression<Func<UResult, TType>> resultProperty, bool typeSafe = true)
        {
            object sourceValue = ReflectionHelper.GetPropertyValue(mapper.sourceModel, ReflectionHelper.GetMemberInfo(sourceProperty).Member.Name);

            PropertyInfo resultPropertyInfo = ReflectionHelper.GetPropertyInfo(typeof(UResult), resultProperty);

            if (typeSafe)
                resultPropertyInfo.SetValue(mapper.resultInstance, sourceValue, null);
            else
            {
                resultPropertyInfo.SetValue(mapper.resultInstance, Convert.ChangeType(sourceValue, resultPropertyInfo.PropertyType), null);
            }

            return this;
        }

        public EntityMapper<TSource, UResult> FinishManuelMapping()
        {
            return mapper;
        }
    }
}