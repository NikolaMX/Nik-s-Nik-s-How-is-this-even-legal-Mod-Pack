import copy
import math

import loader


def run():
    loader.dump_effect({'emitters':[]}, "blank.pfx")
    return loader.loads("""[
         {
           "target" : "/pa/units/land/tank_armor/tank_armor.json",
            "patch" : [
                {"op" : "replace", "path" : "/events/fired/effect_spec", "value" : "/mod/blue_muzzle.pfx socket_muzzle"}
            ]
        },
        {
            "target" : "/blueflamethrower.papa",
            "destination" : "/mod/blueflamethrower.papa"
        },
        {
            "target" : "/pa/units/land/tank_armor/tank_armor_muzzle_flame.pfx",
            "destination" : "/mod/blue_muzzle.pfx",
            "patch" : [
                {"op" : "move", "path" : "/emitters/0/spec/_blue", "from" : "/emitters/0/spec/red"},
                {"op" : "move", "path" : "/emitters/0/spec/red", "from" : "/emitters/0/spec/blue"},
                {"op" : "move", "path" : "/emitters/0/spec/blue", "from" : "/emitters/0/spec/_blue"},

                {"op" : "move", "path" : "/emitters/1/spec/_blue", "from" : "/emitters/1/spec/red"},
                {"op" : "move", "path" : "/emitters/1/spec/red", "from" : "/emitters/1/spec/blue"},
                {"op" : "move", "path" : "/emitters/1/spec/blue", "from" : "/emitters/1/spec/_blue"},

                {"op" : "move", "path" : "/emitters/2/spec/_blue", "from" : "/emitters/2/spec/red"},
                {"op" : "move", "path" : "/emitters/2/spec/red", "from" : "/emitters/2/spec/blue"},
                {"op" : "move", "path" : "/emitters/2/spec/blue", "from" : "/emitters/2/spec/_blue"},

                {"op" : "replace", "path" : "/emitters/1/spec/baseTexture", "value" : "/mod/blueflamethrower.papa"}
            ]
        },
        {
            "target" : "/blank.pfx",
            "destination" : "/mod/inferno/spark_trail.pfx"
        },
        {
            "target" : "/blank.pfx",
            "destination" : "/mod/inferno/spark_hit.pfx"
        }
    ]""")