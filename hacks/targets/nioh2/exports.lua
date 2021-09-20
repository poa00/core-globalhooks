--[[
    Exported configuration settings for Omnified Nioh 2.
    Written By: Matt Weber (https://badecho.com) (https://twitch.tv/omni)
    Copyright 2021 Bad Echo LLC
    
    Bad Echo Technologies are licensed under a
    Creative Commons Attribution-NonCommercial 4.0 International License.
    
    See accompanying file LICENSE.md or a copy at:
    http://creativecommons.org/licenses/by-nc/4.0/
--]]

require("statisticMessages")

function registerExports()
    -- Custom statistics.  
    AdditionalStatistics = function()
        local playerAmrita = toInt(readInteger("playerAmrita"))

        return { 
            WholeStatistic("Amrita (XP)", playerAmrita)
        }
end

end

function unregisterExports()

end