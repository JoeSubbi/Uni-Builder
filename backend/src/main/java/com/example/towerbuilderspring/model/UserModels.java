package com.example.towerbuilderspring.model;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.io.Serializable;
import java.util.Objects;

@Entity
public class UserModels  {

    @EmbeddedId
    private UserModelId userModelId;

    @NotNull
    private int building_xp;

    @NotNull
    private int height;

    @NotNull
    private int primaryColour;

    @NotNull
    private int secondaryColour;

    @NotNull
    private long modelGroup;

    public UserModels() {};

    public UserModels(UserModelId userModelId, int building_xp, int height,int primaryColour, int secondaryColour, int modelGroup) {
        this.userModelId = userModelId;
        this.building_xp = building_xp;
        this.height = height;
        this.primaryColour = primaryColour;
        this.secondaryColour = secondaryColour;
        this.modelGroup = modelGroup;
    }

    @Override
    public String toString() {
        return "UserModels{" +
                "userModelId=" + userModelId +
                ", building_xp=" + building_xp +
                ", height=" + height +
                ", primaryColour=" + primaryColour +
                ", secondaryColour=" + secondaryColour +
                ", modelGroup=" + modelGroup +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (!(o instanceof UserModels)) return false;
        UserModels that = (UserModels) o;
        return building_xp == that.building_xp &&
                height == that.height &&
                primaryColour == that.primaryColour &&
                secondaryColour == that.secondaryColour &&
                modelGroup == that.modelGroup &&
                Objects.equals(userModelId, that.userModelId);
    }

    @Override
    public int hashCode() {
        return Objects.hash(userModelId, building_xp, height, primaryColour, secondaryColour, modelGroup);
    }

    public UserModelId getUserModelId() {
        return userModelId;
    }

    public void setUserModelId(UserModelId userModelId) {
        this.userModelId = userModelId;
    }

    public int getBuilding_xp() {
        return building_xp;
    }

    public void setBuilding_xp(int building_xp) {
        this.building_xp = building_xp;
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

    public long getModelGroup() {
        return modelGroup;
    }

    public void setModelGroup(long modelGroup) {
        this.modelGroup = modelGroup;
    }
}

//@Entity
//public class UserModels {
//
//    @EmbeddedId
//    private UserModelId userModelId;
//
//    @NotNull
//    private int building_xp;
//
//    @NotNull
//    private int height;
//
//    @NotNull
//    private int primaryColour;
//
//    @NotNull
//    private int secondaryColour;
//
//    @NotNull
//    private long modelGroup;
//
//    private LocalDateTime createdAt;
//
//
//    public UserModels() {};
//
//    public UserModels(UserModelId userModelId, int building_xp, int height,int primaryColour, int secondaryColour, int modelGroup) {
//        this.userModelId = userModelId;
//        this.building_xp = building_xp;
//        this.height = height;
//        this.primaryColour = primaryColour;
//        this.secondaryColour = secondaryColour;
//        this.modelGroup = modelGroup;
//    }
//
//    @Override
//    public String toString() {
//        return "UserModels{" +
//                "userModelId=" + userModelId.toString() +
//                ", building_xp=" + building_xp +
//                ", height=" + height +
//                ", primaryColour=" + primaryColour +
//                ", secondaryColour=" + secondaryColour +
//                ", modelGroup=" + modelGroup +
//                '}';
//    }
//
//    public UserModelId getUserModelId() {
//        return userModelId;
//    }
//
//    public void setUserModelId(UserModelId userModelId) {
//        this.userModelId = userModelId;
//    }
//
//    public int getBuilding_xp() {
//        return building_xp;
//    }
//
//    public void setBuilding_xp(int building_xp) {
//        this.building_xp = building_xp;
//    }
//
//    public int getHeight() {
//        return height;
//    }
//
//    public void setHeight(int height) {
//        this.height = height;
//    }
//
//    public int getPrimaryColour() {
//        return primaryColour;
//    }
//
//    public void setPrimaryColour(int primaryColour) {
//        this.primaryColour = primaryColour;
//    }
//
//    public int getSecondaryColour() {
//        return secondaryColour;
//    }
//
//    public void setSecondaryColour(int secondaryColour) {
//        this.secondaryColour = secondaryColour;
//    }
//
//    public long getModelGroup() {
//        return modelGroup;
//    }
//
//    public void setModelGroup(long modelGroup) {
//        this.modelGroup = modelGroup;
//    }
//
//    public LocalDateTime getCreatedAt() {
//        return createdAt;
//    }
//
//    public void setCreatedAt(LocalDateTime createdAt) {
//        this.createdAt = createdAt;
//    }
//}
