package com.example.towerbuilderspring.model;


import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

@Entity
@Validated
public class BuildingModels {

    @Id
    @NotNull
    private long buildingCode;

    @NotNull
    @Column(unique = true)
    private String buildingName;

    @NotNull
    private int building_xp;

    // Height only exists for the custom building.
    @NotNull
    private int height;

    @NotNull
    private int primaryColour;

    @NotNull
    private int secondaryColour;

    @NotNull
    private long model;

    public BuildingModels() {};

    public BuildingModels(long buildingCode, String buildingName, long modelGroup, int building_xp, int primaryColour, int secondaryColour) {
        this.buildingCode = buildingCode;
        this.buildingName = buildingName;
        this.model = modelGroup;
        this.building_xp = building_xp;
        this.primaryColour = primaryColour;
        this.secondaryColour = secondaryColour;
    }

    public int getHeight() {
        return height;
    }

    public void setHeight(int height) {
        this.height = height;
    }

    public int getPrimaryColour() {
        return primaryColour;
    }

    public void setPrimaryColour(int primaryColour) {
        this.primaryColour = primaryColour;
    }

    public int getSecondaryColour() {
        return secondaryColour;
    }

    public void setSecondaryColour(int secondaryColour) {
        this.secondaryColour = secondaryColour;
    }

    public int getBuilding_xp() {
        return building_xp;
    }

    public void setBuilding_xp(int building_xp) {
        this.building_xp = building_xp;
    }

    public long getBuildingCode() {
        return buildingCode;
    }

    public void setBuildingCode(long buildingCode) {
        this.buildingCode = buildingCode;
    }

    public String getBuildingName() {
        return buildingName;
    }

    public void setBuildingName(String buildingName) {
        this.buildingName = buildingName;
    }

    public long getModelGroup() {
        return model;
    }

    public void setModelGroup(long modelGroup) {
        this.model = modelGroup;
    }

    @Override
    public String toString() {
        return "BuildingModels{" +
                "buildingCode=" + buildingCode +
                ", buildingName='" + buildingName + '\'' +
                ", modelGroup=" + model +
//                ", users=" + users +
                '}';
    }
}
