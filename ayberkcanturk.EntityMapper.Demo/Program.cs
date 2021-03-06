﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityMapper.Demo.Entity;

namespace EntityMapper.Demo
{
    class Program
    {
        //Hello World
        static void Main(string[] args)
        {
            Human human = new Human();
            human.Name = "ayberk";
            human.Age = 29;
            human.Sex = 'M';

            EntityMapper.Default.EntityMapper<Human, Robot> mapper = new Default.EntityMapper<Human, Robot>();
            
            Robot robot = mapper.AutoMap(human).Result();

            Robot robot2 = mapper.ManualMap(human)
                .ManualPropertyMap(x => x.Age, y => y.Age)
                .ManualPropertyMap(x => x.Name, y => y.Name)
                .FinishManuelMapping()
                .Result();

            EntityMapper.Default.EntityMapper<Human, Animal> mapper2 = new Default.EntityMapper<Human, Animal>();

            Animal robot3 = mapper2.ManualMap(human)
                .ManualPropertyMap(x => x.Age, y => y.animalAge, false)
                .ManualPropertyMap(x => x.Name, y => y.aminalName)
                .FinishManuelMapping()
                .Result();

            EntityMapper.Default.EntityMapper<Human, Alien> mapper3 = new Default.EntityMapper<Human, Alien>();
            Alien robot4 = mapper3.AutoMap(human, 3).Result();

            EntityMapper.Default.EntityMapper<Human, Robot> mapper4 = new Default.EntityMapper<Human, Robot>();
            Robot robot5 = mapper4.AutoMap(human).ManualMap(human).ManualPropertyMap(x=>x.Name, y=>y.Name).FinishManuelMapping().Result();

            Human human2 = new Human();
            human2.Name = "mehmet";
            human2.Sex = 'M';

            Robot robot6 = mapper4.AutoMap(human2).ManualMap(human2).ManualPropertyMap(x => x.Age, y => y.Age).FinishManuelMapping().Result();


            mapper.Dispose();
        }
    }
}